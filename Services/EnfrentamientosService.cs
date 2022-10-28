using webapi.Models;
namespace webapi.Services;

public class EnfrentamientosService : IEnfrentamientosService {
    
    SupersContext context;

    public EnfrentamientosService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Enfrentamiento> Get() {
        return context.Enfrentamientos;
    }

    public async Task Save(Enfrentamiento enfrentamiento) {
        context.Add(enfrentamiento);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Enfrentamiento enfrentamiento) {
        var enfrentamientoActual = context.Enfrentamientos.Find(id);

        if(enfrentamientoActual != null) {
            enfrentamientoActual.titulo = enfrentamiento.titulo;
            enfrentamientoActual.fecha = enfrentamiento.fecha;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var enfrentamientoActual = context.Enfrentamientos.Find(id);

        if(enfrentamientoActual != null) {
            context.Remove(enfrentamientoActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface IEnfrentamientosService {
    IEnumerable<Enfrentamiento> Get();
    Task Save(Enfrentamiento enfrentamiento);

    Task Update(Guid id, Enfrentamiento enfrentamiento);

    Task Delete(Guid id);
}