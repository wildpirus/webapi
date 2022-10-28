using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class SupersService : ISupersService {
    
    SupersContext context;

    public SupersService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Super> Get() {
        return context.Supers;
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
        //Console.WriteLine(context.("select super.super_id, super.nombre, string_agg(rasgo.titulo, ',' ORDER BY rasgo.titulo) habilidades from super join rasgo_super on rasgo_super.super_id = super.super_id join rasgo on rasgo.rasgo_id = rasgo_super.rasgo_id group by super.super_id, super.nombre"));
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
    IEnumerable<Super> GetHeroe(Guid id);
    IEnumerable<Super> GetVillano(Guid id);
    IEnumerable<Super> GetHeroeByNombre(string nombre);
    IEnumerable<Super> GetVillanoByNombre(string nombre);
    IEnumerable<Super> GetHeroeByHabilidades(string habilidades);
    Task Save(Super super);

    Task Update(Guid id, Super super);

    Task Delete(Guid id);
}