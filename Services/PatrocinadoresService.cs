using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class PatrocinadoresService : IPatrocinadoresService {
    
    SupersContext context;

    public PatrocinadoresService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Patrocinador> Get() {
        return context.Patrocinadores;
    }

    public async Task Save(Patrocinador patrocinador) {
        int exist = context.Patrocinadores.Where(p => p.nombre == patrocinador.nombre && p.super_id == patrocinador.super_id).Count();
        if (exist == 0) {
            patrocinador.patrocinador_id = Guid.NewGuid();
            context.Add(patrocinador);
            context.SaveChanges();
        }
    }

    public async Task Update(Guid id, Patrocinador patrocinador) {
        var patrocinadorActual = context.Patrocinadores.Find(id);

        if(patrocinadorActual != null) {
            patrocinadorActual.super_id = patrocinador.super_id;
            patrocinadorActual.nombre = patrocinador.nombre;
            patrocinadorActual.monto = patrocinador.monto;
            patrocinadorActual.origen_monto = patrocinador.origen_monto;

            context.SaveChanges();
        }
    }

    public async Task Delete(Guid id) {
        var patrocinadorActual = context.Patrocinadores.Find(id);

        if(patrocinadorActual != null) {
            context.Remove(patrocinadorActual);
            context.SaveChanges();
        }
    }
}

public interface IPatrocinadoresService {
    IEnumerable<Patrocinador> Get();
    Task Save(Patrocinador patrocinador);

    Task Update(Guid id, Patrocinador patrocinador);

    Task Delete(Guid id);
}