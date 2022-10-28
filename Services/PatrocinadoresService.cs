using webapi.Models;
namespace webapi.Services;

public class PatrocinadoresService : IPatrocinadoresService {
    
    SupersContext context;

    public PatrocinadoresService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Patrocinador> Get() {
        return context.Patrocinadores;
    }

    public async Task Save(Patrocinador patrocinador) {
        context.Add(patrocinador);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Patrocinador patrocinador) {
        var patrocinadorActual = context.Patrocinadores.Find(id);

        if(patrocinadorActual != null) {
            patrocinadorActual.super_id = patrocinador.super_id;
            patrocinadorActual.nombre = patrocinador.nombre;
            patrocinadorActual.monto = patrocinador.monto;
            patrocinadorActual.origen_monto = patrocinador.origen_monto;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var patrocinadorActual = context.Patrocinadores.Find(id);

        if(patrocinadorActual != null) {
            context.Remove(patrocinadorActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface IPatrocinadoresService {
    IEnumerable<Patrocinador> Get();
    Task Save(Patrocinador patrocinador);

    Task Update(Guid id, Patrocinador patrocinador);

    Task Delete(Guid id);
}