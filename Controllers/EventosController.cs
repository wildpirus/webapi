using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class EventosController: ControllerBase {
    IEventosService eventosService;

    public EventosController(IEventosService service) {
        eventosService = service;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(eventosService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Evento evento) {
        eventosService.Save(evento);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Evento evento) {
        eventosService.Update(id, evento);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        eventosService.Delete(id);
        return Ok();
    }    

}