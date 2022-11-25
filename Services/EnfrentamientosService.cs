using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class EnfrentamientosService : IEnfrentamientosService {
    
    SupersContext context;

    public EnfrentamientosService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public object Get() {
        List<Object> luchas = new List<object>();
        foreach (Enfrentamiento lucha in context.Enfrentamientos.Include(e => e.Peleas).ThenInclude(p => p.Super).ToList()) {
            List<Pelea> peleas = lucha.Peleas.ToList();
            List<Pelea> heroes = peleas.Where(p => p.Super.rol_super == "Heroe").ToList();
            List<Pelea> villanos = peleas.Where(p => p.Super.rol_super == "Villano").ToList();
            luchas.Add(new {
                enfrentamiento_id = lucha.enfrentamiento_id,
                titulo = lucha.titulo,
                fecha = lucha.fecha,
                Peleas = new {
                    Heroes = heroes,
                    Villanos = villanos
                }
            });
        }
        return luchas;//context.Enfrentamientos.Include(e => e.Peleas).ThenInclude(p => p.Super).ToList();
    }

    public async Task Save(Enfrentamiento enfrentamiento) {
        int exist = context.Enfrentamientos.Where(e => e.titulo == enfrentamiento.titulo).Count();
        if (exist == 0) {
            enfrentamiento.enfrentamiento_id = Guid.NewGuid();
            context.Add(enfrentamiento);
            context.SaveChanges();
        }
    }

    public async Task Update(Guid id, Enfrentamiento enfrentamiento) {
        var enfrentamientoActual = context.Enfrentamientos.Find(id);

        if(enfrentamientoActual != null) {
            enfrentamientoActual.titulo = enfrentamiento.titulo;
            enfrentamientoActual.fecha = enfrentamiento.fecha;

            context.SaveChanges();
        }
    }

    public async Task Delete(Guid id) {
        var enfrentamientoActual = context.Enfrentamientos.Find(id);

        if(enfrentamientoActual != null) {
            context.Remove(enfrentamientoActual);
            context.SaveChanges();
        }
    }
}

public interface IEnfrentamientosService {
    Object Get();
    Task Save(Enfrentamiento enfrentamiento);

    Task Update(Guid id, Enfrentamiento enfrentamiento);

    Task Delete(Guid id);
}