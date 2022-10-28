using webapi.Models;
namespace webapi.Services;

public class EventosService : IEventosService {
    
    SupersContext context;

    public EventosService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Evento> Get() {
        return context.Eventos;
    }

    public async Task Save(Evento evento) {
        context.Add(evento);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Evento evento) {
        var eventoActual = context.Eventos.Find(id);

        if(eventoActual != null) {
            eventoActual.super_id = evento.super_id;
            eventoActual.titulo = evento.titulo;
            eventoActual.inicio = evento.inicio;
            eventoActual.fin = evento.fin;
            eventoActual.descripcion = evento.descripcion;
            eventoActual.lugar = evento.lugar;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id) {
        var eventoActual = context.Eventos.Find(id);

        if(eventoActual != null) {
            context.Remove(eventoActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface IEventosService {
    IEnumerable<Evento> Get();
    Task Save(Evento evento);

    Task Update(Guid id, Evento evento);

    Task Delete(Guid id);
}