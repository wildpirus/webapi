using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class RasgosSupersService : IRasgosSupersService {
    SupersContext context;

    public RasgosSupersService(SupersContext dbcontext) {
        context = dbcontext;
    }

     public IEnumerable<RasgoSuper> Get() {
        return context.RasgosSupers;
    }

    public async Task Save(RasgoSuper rasgoSuper) {
        int exist = context.RasgosSupers.Where(r => r.rasgo_id == rasgoSuper.rasgo_id && r.super_id == rasgoSuper.super_id).Count();
        if (exist == 0){
            rasgoSuper.rasgo_super_id = Guid.NewGuid();
            context.Add(rasgoSuper);
            context.SaveChanges();
        }
    }

    public async Task Update(Guid id, RasgoSuper rasgoSuper) {
        var rasgoSuperActual = context.RasgosSupers.Find(id);

        if(rasgoSuperActual != null) {
            rasgoSuperActual.super_id = rasgoSuper.super_id;
            rasgoSuper.rasgo_id = rasgoSuper.rasgo_id;
            
            context.SaveChanges();
        }
    }

    public async Task Delete(Guid id) {
        var rasgoSuperActual = context.RasgosSupers.Find(id);

        if(rasgoSuperActual != null) {
            context.Remove(rasgoSuperActual);
            context.SaveChanges();
        }
    }

}

public interface IRasgosSupersService {
    IEnumerable<RasgoSuper> Get();
    Task Save(RasgoSuper rasgoSuper);

    Task Update(Guid id, RasgoSuper rasgoSuper);

    Task Delete(Guid id);
}