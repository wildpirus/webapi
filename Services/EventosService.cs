using webapi.Models;
namespace webapi.Services;
using Microsoft.EntityFrameworkCore;

public class EventosService : IEventosService {
    
    SupersContext context;

    public EventosService (SupersContext dbcontext) {
        context = dbcontext;
    }

    public IEnumerable<Evento> GetByUser(Guid id, string inicio, string fin) {
        IEnumerable<Evento> eventos = context.Eventos.Where(p => p.super_id == id);
        if (inicio != null){
            DateTime start;
            DateTime.TryParse(inicio, out start);
            eventos = eventos.Where(e => e.inicio >= start);
        }if (fin != null){
            DateTime finish;
            DateTime.TryParse(fin, out finish);
            eventos = eventos.Where(e => e.fin <= finish);
        }
        return eventos;
    }

    public Evento Get(Guid id) {
        return context.Eventos.Where(p => p.evento_id == id).First();
    }

    public async Task Save(Evento evento) {
        int exist = context.Eventos.Where(e => e.super_id == evento.super_id && 
            (evento.inicio >= e.inicio && evento.inicio <= e.fin) &&  
            (evento.fin >= e.inicio && evento.fin <= e.fin)
        ).ToList().Count();
        if (exist == 0) {
            evento.evento_id = Guid.NewGuid(); 
            Console.WriteLine(evento.evento_id.ToString());
            Console.WriteLine(evento.super_id.ToString());
            Console.WriteLine(evento.titulo);
            Console.WriteLine(evento.inicio);
            Console.WriteLine(evento.fin);
            Console.WriteLine(evento.descripcion);
            Console.WriteLine(evento.lugar);
            context.Add(evento);
            context.SaveChanges();
        }
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

            context.SaveChanges();
        }
    }

    public async Task Delete(Guid id) {
        var eventoActual = context.Eventos.Find(id);

        if(eventoActual != null) {
            context.Remove(eventoActual);
            context.SaveChanges();
        }
    }
}

public interface IEventosService {
    IEnumerable<Evento> GetByUser(Guid id, string from, string to);
    Evento Get(Guid id);
    Task Save(Evento evento);

    Task Update(Guid id, Evento evento);

    Task Delete(Guid id);
}