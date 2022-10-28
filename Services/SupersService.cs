using webapi.Models;
namespace webapi.Services;

public class SupersService : ISupersService {
    
    SupersContext context;

    public SupersService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Super> Get() {
        return context.Supers;
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
    IEnumerable<Super> Get();
    Task Save(Super super);

    Task Update(Guid id, Super super);

    Task Delete(Guid id);
}