using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class EnfrentamientosController: ControllerBase {
    IEnfrentamientosService enfrentamientosService;

    public EnfrentamientosController(IEnfrentamientosService service) {
        enfrentamientosService = service;
    }

    [HttpGet("luchas")]
    public IActionResult Get() {
        return Ok(enfrentamientosService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Enfrentamiento enfrentamiento) {
        enfrentamientosService.Save(enfrentamiento);
        if (enfrentamiento.enfrentamiento_id == Guid.Empty){
            return Conflict();
        }else {
            return Created(new Uri("https://www.google.com/"),enfrentamiento);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Enfrentamiento enfrentamiento) {
        enfrentamientosService.Update(id, enfrentamiento);
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        enfrentamientosService.Delete(id);
        return NoContent();
    }    

}