using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class RasgosService : IRasgosService{
    
    SupersContext context;

    public RasgosService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Rasgo> GetHabilidades(){
        return context.Rasgos.Where(h => h.tipo_rasgo == "Habilidad");
    }
    public IEnumerable<Rasgo> GetDebilidades(){
        return context.Rasgos.Where(h => h.tipo_rasgo == "Debilidad");
    }
    public Rasgo GetRasgo(Guid id){
        return context.Rasgos.Where(h => h.rasgo_id == id).First();
    }

    public async Task Save(Rasgo rasgo) {
        int exist = context.Rasgos.Where(r => r.titulo.ToLower() == rasgo.titulo.ToLower() && r.tipo_rasgo == rasgo.tipo_rasgo).Count();
        if (exist == 0) {
            rasgo.rasgo_id = Guid.NewGuid();
            context.Add(rasgo);
            context.SaveChanges();
        }
    }

    public async Task Update(Guid id, Rasgo rasgo) {
        var rasgoActual = context.Rasgos.Find(id);

        if(rasgoActual != null) {
            rasgoActual.titulo = rasgo.titulo;
            rasgoActual.tipo_rasgo = rasgo.tipo_rasgo;

            context.SaveChanges();
        }
    }

    public async Task Delete(Guid id) {
        var rasgoActual = context.Rasgos.Find(id);

        if(rasgoActual != null) {
            context.Remove(rasgoActual);
            context.SaveChanges();
        }
    }
}

public interface IRasgosService {
    IEnumerable<Rasgo> GetHabilidades();
    IEnumerable<Rasgo> GetDebilidades();
    Rasgo GetRasgo(Guid id);
    Task Save(Rasgo super);

    Task Update(Guid id, Rasgo super);

    Task Delete(Guid id);
}