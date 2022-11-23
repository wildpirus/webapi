using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class SupersService : ISupersService {
    
    SupersContext context;

    public SupersService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Super> GetHeroes() {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Heroe").ToList();
    }

    public IEnumerable<Super> GetVillanos() {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Villano").ToList();
    }

    public Super GetHeroe(Guid id) {
        return context.Supers.Include(p => p.Patrocinadores).Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo)
        .Where(p => p.rol_super == "Heroe")
        .Where(p => p.super_id == id).First();
    }

    public Super GetVillano(Guid id) {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo)
        .Where(p => p.rol_super == "Villano")
        .Where(p => p.super_id == id).First();
    }

    public IEnumerable<Super> GetHeroeByNombre(string nombre) {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo)
        .Where(p => p.rol_super == "Heroe")
        .Where(p => p.nombre.Contains(nombre)).ToList();
    }

    public IEnumerable<Super> GetVillanoByNombre(string nombre) {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo)
        .Where(p => p.rol_super == "Villano")
        .Where(p => p.nombre.Contains(nombre)).ToList();
    }

    public IEnumerable<Super>  GetHeroeByHabilidades(string habilidades) {
        string[] hab = habilidades.Split(",");
        List<Super> matchHeroes = new List<Super>();
        foreach (Super super in context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Heroe").ToList()) {
            ICollection<Rasgo> rasgosSuper = super.RasgosSuper.Select(x => x.Rasgo).ToList();
            HashSet<string> s = new HashSet<string>();
            foreach (Rasgo rasgo in rasgosSuper) {
                if (rasgo.tipo_rasgo == "Habilidad") {
                    s.Add(rasgo.titulo);
                }
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
            Villano = context.Supers.Where(v => v.super_id == query.Supers.Villano).First(),
            Heroe = context.Supers.Where(v => v.super_id == query.Supers.Heroe).First(),
            Peleas = query.Peleas
        };
    }

    public IEnumerable<Super> GetTeenHeroes () {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Heroe" && x.edad > 9 && x.edad < 20).ToList();
    }

    public IEnumerable<Super> GetAdultHeroes () {
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Heroe" && x.edad > 19).ToList();
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
            Villano = context.Supers.Where(v => v.super_id == query.Villano).First(),
            Peleas = query.Peleas
        };
    }

    public IEnumerable<Super> GetHeroeByRelacionesPersonales(string relaciones){
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo)
            .Where(x => x.rol_super == "Heroe")
            .Where(x => x.relaciones.Contains(relaciones)).ToList();
    }

    public IEnumerable<Super> GetVillanoByOrigen(string origen){
        return context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo)
            .Where(x => x.rol_super == "Villano")
            .Where(x => x.origen.ToLower().Contains(origen.ToLowerInvariant())).ToList();
    }

    public IEnumerable<Super> GetVillanoByDebilidades(string debilidades) {
        string[] deb = debilidades.Split(",");
        List<Super> matchVillanos = new List<Super>();
        foreach (Super super in context.Supers.Include(x => x.RasgosSuper).ThenInclude(r => r.Rasgo).Where(x => x.rol_super == "Villano").ToList()) {
            ICollection<Rasgo> rasgosSuper = super.RasgosSuper.Select(x => x.Rasgo).ToList();
            HashSet<string> s = new HashSet<string>();
            foreach (Rasgo rasgo in rasgosSuper) {
                if (rasgo.tipo_rasgo == "Debilidad") {
                    s.Add(rasgo.titulo);
                }
            }
            int p = s.Count;
            foreach (string debilidad in deb){
                s.Add(debilidad);
            }
            if (s.Count == p) {
                matchVillanos.Add(super);
            }
        }
        return matchVillanos;
    }

    public Patrocinador GetHeroeBestSponsor(Guid id) {
        return context.Supers.Include(x => x.Patrocinadores).Where(x => x.rol_super == "Heroe" && x.super_id == id).First()
        .Patrocinadores.OrderByDescending(p => p.monto).First();
    }

    public IEnumerable<Super> Top3Heroes(){
        var query = (from pelea in context.Peleas
        join heroe in context.Supers
        on pelea.super_id equals heroe.super_id
        where heroe.rol_super == "Heroe" && pelea.gana
        group pelea by pelea.super_id into g
        select  new {
            Heroe = g.Key,
            Count = g.Count()
        }).OrderByDescending(p => p.Count).Take(3).ToList();

        List<Super> best3Heroes = new List<Super>();
        foreach (var item in query){
            //Console.WriteLine("Heroe {0}: {1}", item.Heroe, item.Count);
            best3Heroes.Add(context.Supers.Include(x => x.Peleas).ThenInclude(p => p.Enfrentamiento).Where(x => x.super_id == item.Heroe).First());
        }

        
        return best3Heroes;
    }

    public Object mostFoughtVillanoByHeroe(Guid id) {
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
            villano.rol_super == "Villano"
        )group villano by villano.super_id into p
        select new {
            Villano = p.Key,
            Peleas = p.Count()
        }).OrderByDescending(x => x.Peleas).First();

        //Console.WriteLine("Villano {0}, peleas: {1}",query.Villano,query.Peleas);
        var atx = (from heroe in context.Supers
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
            villano.rol_super == "Villano" &&
            villano.super_id == query.Villano
        )select enfrentamiento).Include(e => e.Peleas).ThenInclude(p => p.Super).ToList();
        return new {
            Villano = context.Supers/*.Include(v => v.Peleas.Where(p => p.Enfrentamiento.Peleas.Select(f => f.super_id).Where(y => y == id).First() == id)).ThenInclude(p => p.Enfrentamiento)*/.Where(s => s.super_id == query.Villano).First(),//context.Supers.Where(v => v.super_id == query.Villano).First(),
            Numero = query.Peleas,
            Peleas = atx //context.Enfrentamientos.Include(e => e.Peleas)
        };
    }

    public IEnumerable<Super> getPatrocinadoresByHeroe() {
        return context.Supers.Include(p => p.Patrocinadores).Where(h => h.rol_super == "Heroe").ToList();
    }           

    public async Task Save(Super super) {
        int exist = context.Supers.Where(s => s.nombre == super.nombre).Count();
        if (exist == 0) {
            super.super_id = Guid.NewGuid();
            super.nombre = super.nombre.ToLowerInvariant();
            super.relaciones = super.relaciones.ToLowerInvariant();
            super.origen = super.origen.ToLowerInvariant();
            await context.AddAsync(super);
            await context.SaveChangesAsync();
        }
    }

    public async Task Update(Guid id, Super super) {
        var superActual = context.Supers.Find(id);

        if(superActual != null) {
            superActual.nombre = super.nombre.ToLowerInvariant();
            superActual.rol_super = super.rol_super;
            superActual.image_link = super.image_link;
            superActual.relaciones = super.relaciones.ToLowerInvariant();
            superActual.origen = super.origen.ToLowerInvariant();

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
    Super GetHeroe(Guid id);
    Super GetVillano(Guid id);
    IEnumerable<Super> GetHeroeByNombre(string nombre);
    IEnumerable<Super> GetVillanoByNombre(string nombre);
    IEnumerable<Super> GetHeroeByHabilidades(string habilidades);
    Object GetWorstVillainAgainstTeen();
    IEnumerable<Super> GetTeenHeroes();
    IEnumerable<Super> GetAdultHeroes();
    Object GetWorstVillainAgainstTeenHero(Guid id);
    IEnumerable<Super> GetHeroeByRelacionesPersonales(string relaciones);
    IEnumerable<Super> GetVillanoByOrigen(string origen);
    IEnumerable<Super>  GetVillanoByDebilidades(string debilidades);
    Patrocinador GetHeroeBestSponsor(Guid id);
    IEnumerable<Super> Top3Heroes();
    Object mostFoughtVillanoByHeroe(Guid id);
    IEnumerable<Super> getPatrocinadoresByHeroe();
    Task Save(Super super);
    Task Update(Guid id, Super super);
    Task Delete(Guid id);
}