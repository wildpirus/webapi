using webapi.Models;
namespace webapi.Services;

public class RasgosSupersService : IRasgosSupersService {
    SupersContext context;

    public RasgosSupersService(SupersContext dbcontext) {
        context = dbcontext;
    }

     public IEnumerable<RasgoSuper> Get() {
        return context.RasgosSupers;
    }

    public async Task Save(RasgoSuper rasgoSuper) {
        
        context.Add(rasgoSuper);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, RasgoSuper rasgoSuper) {
        var rasgoSuperActual = context.RasgosSupers.Find(id);

        if(rasgoSuperActual != null) {
            rasgoSuperActual.super_id = rasgoSuper.super_id;
            rasgoSuper.rasgo_id = rasgoSuper.rasgo_id;
            
            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var rasgoSuperActual = context.RasgosSupers.Find(id);

        if(rasgoSuperActual != null) {
            context.Remove(rasgoSuperActual);
            await context.SaveChangesAsync();
        }
    }

}

public interface IRasgosSupersService {
    IEnumerable<RasgoSuper> Get();
    Task Save(RasgoSuper rasgoSuper);

    Task Update(Guid id, RasgoSuper rasgoSuper);

    Task Delete(Guid id);
}