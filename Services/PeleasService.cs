using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class PeleasService : IPeleasService {
    
    SupersContext context;

    public PeleasService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Pelea> Get() {
        return context.Peleas;
    }

    public async Task Save(Pelea pelea) {
        int exist = context.Peleas.Where(p => p.enfrentamiento_id == pelea.enfrentamiento_id && p.super_id == pelea.super_id).Count();
        if (exist == 0){
            pelea.pelea_id = Guid.NewGuid();
            await context.AddAsync(pelea);
            await context.SaveChangesAsync();
        }
    }

    public async Task Update(Guid id, Pelea pelea) {
        var peleaActual = context.Peleas.Find(id);

        if(peleaActual != null) {
            peleaActual.super_id = pelea.super_id;
            peleaActual.enfrentamiento_id = pelea.enfrentamiento_id;
            peleaActual.gana = pelea.gana;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var peleaActual = context.Peleas.Find(id);

        if(peleaActual != null) {
            context.Remove(peleaActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface IPeleasService {
    IEnumerable<Pelea> Get();
    Task Save(Pelea pelea);

    Task Update(Guid id, Pelea pelea);

    Task Delete(Guid id);
}