using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class SupersService : ISupersService {
    
    SupersContext context;

    public SupersService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Super> GetHeroes() {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Heroe");
    }

    public IEnumerable<Super> GetVillanos() {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Villano");
    }

    public IEnumerable<Super> GetHeroe(Guid id) {
        return context.Supers.Where(p => p.rol_super == "Heroe")
        .Where(p => p.super_id == id);
    }

    public IEnumerable<Super> GetVillano(Guid id) {
        return context.Supers.Where(p => p.rol_super == "Villano")
        .Where(p => p.super_id == id);
    }

    public IEnumerable<Super> GetHeroeByNombre(string nombre) {
        return context.Supers.Where(p => p.rol_super == "Heroe")
        .Where(p => p.nombre.Contains(nombre));
    }

    public IEnumerable<Super> GetVillanoByNombre(string nombre) {
        return context.Supers.Where(p => p.rol_super == "Villano")
        .Where(p => p.nombre.Contains(nombre));
    }

    public IEnumerable<Super>  GetHeroeByHabilidades(string habilidades) {
        string[] hab = habilidades.Split(",");
        List<Super> matchHeroes = new List<Super>();
        foreach (Super super in context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Heroe")) {
            ICollection<Rasgo> rasgosSuper = super.RasgosSuper.Select(x => x.Rasgo).ToList();
            HashSet<string> s = new HashSet<string>();
            foreach (Rasgo rasgo in rasgosSuper) {
                s.Add(rasgo.titulo);
            }
            int p = s.Count;
            foreach (string habilidad in hab){
                s.Add(habilidad);
            }
            if (s.Count == p) {
                matchHeroes.Add(super);
            }
        }
        return matchHeroes;
    }

    public Object GetWorstVillainAgainstTeen() {
        var query = (from pelea in context.Peleas
        join villano in context.Supers
        on pelea.super_id equals villano.super_id 
        join enfrentamiento in context.Enfrentamientos 
        on pelea.enfrentamiento_id equals enfrentamiento.enfrentamiento_id 
        join fight in context.Peleas
        on enfrentamiento.enfrentamiento_id equals fight.enfrentamiento_id 
        join heroe in context.Supers
        on fight.super_id equals heroe.super_id
        where (
            heroe.rol_super == "Heroe" && 
            heroe.edad > 9 && 
            heroe.edad < 20 && 
            fight.gana == true && 
            villano.rol_super == "Villano" && 
            pelea.gana == false
        )select new { 
            Villano = villano.super_id,
            Heroe = heroe.super_id,
            Fight = fight.pelea_id
        }).GroupBy(x => new {x.Villano, x.Heroe} ).Select(x => new {
            Supers = x.Key,
            Peleas = x.Count()
        }).OrderByDescending(x => x.Peleas).First();

        return new {
            Villano = context.Supers.Where(v => v.super_id == query.Supers.Villano),
            Heroe = context.Supers.Where(v => v.super_id == query.Supers.Heroe),
            Peleas = query.Peleas
        };
    }

    public IEnumerable<Super> GetTeenHeroes () {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Heroe" && x.edad > 9 && x.edad < 20);
    }

    public Object GetWorstVillainAgainstTeenHero(Guid id) {
        var query = (from heroe in context.Supers
        join pelea in context.Peleas
        on heroe.super_id equals pelea.super_id
        join enfrentamiento in context.Enfrentamientos
        on pelea.enfrentamiento_id equals enfrentamiento.enfrentamiento_id
        join fight in context.Peleas
        on enfrentamiento.enfrentamiento_id equals fight.enfrentamiento_id
        join villano in context.Supers
        on fight.super_id equals villano.super_id
        where (
            heroe.rol_super == "Heroe" && 
            heroe.super_id == id && 
            heroe.edad > 9 && 
            heroe.edad < 20 &&
            pelea.gana &&
            !fight.gana &&
            villano.rol_super == "Villano" 
        )select new { 
            Villano = villano.super_id,
            Fight = fight.pelea_id
        }).GroupBy(x => x.Villano ).Select(x => new {
            Villano = x.Key,
            Peleas = x.Count()
        }).OrderByDescending(x => x.Peleas).First();

        return new {
            Villano = context.Supers.Where(v => v.super_id == query.Villano),
            Peleas = query.Peleas
        };
    }

    public async Task Save(Super super) {
        context.Add(super);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Super super) {
        var superActual = context.Supers.Find(id);

        if(superActual != null) {
            superActual.nombre = super.nombre;
            superActual.rol_super = super.rol_super;
            superActual.relaciones = super.relaciones;
            superActual.origen = super.origen;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var superActual = context.Supers.Find(id);

        if(superActual != null) {
            context.Remove(superActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface ISupersService {

    IEnumerable<Super> GetHeroes();
    IEnumerable<Super> GetVillanos();
    IEnumerable<Super> GetHeroe(Guid id);
    IEnumerable<Super> GetVillano(Guid id);
    IEnumerable<Super> GetHeroeByNombre(string nombre);
    IEnumerable<Super> GetVillanoByNombre(string nombre);
    IEnumerable<Super> GetHeroeByHabilidades(string habilidades);
    Object GetWorstVillainAgainstTeen();
    IEnumerable<Super> GetTeenHeroes();
    Object GetWorstVillainAgainstTeenHero(Guid id);
    Task Save(Super super);
    Task Update(Guid id, Super super);
    Task Delete(Guid id);
}