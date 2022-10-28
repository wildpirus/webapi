using webapi.Models;
namespace webapi.Services;

public class RasgosService : IRasgosService{
    
    SupersContext context;

    public RasgosService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Rasgo> Get() {
        return context.Rasgos;
    }

    public async Task Save(Rasgo rasgo) {
        context.Add(rasgo);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Rasgo rasgo) {
        var rasgoActual = context.Rasgos.Find(id);

        if(rasgoActual != null) {
            rasgoActual.titulo = rasgo.titulo;
            rasgoActual.tipo_rasgo = rasgo.tipo_rasgo;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var rasgoActual = context.Rasgos.Find(id);

        if(rasgoActual != null) {
            context.Remove(rasgoActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface IRasgosService {
    IEnumerable<Rasgo> Get();
    Task Save(Rasgo super);

    Task Update(Guid id, Rasgo super);

    Task Delete(Guid id);
}