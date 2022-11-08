using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class PeleasController: ControllerBase {
    IPeleasService peleasService;

    public PeleasController(IPeleasService service) {
        peleasService = service;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(peleasService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Pelea pelea) {
        peleasService.Save(pelea);
        if (pelea.pelea_id == Guid.Empty){
            return Conflict();
        }else {
            return Created(new Uri("https://www.google.com/"),pelea);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Pelea pelea) {
        peleasService.Update(id, pelea);
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        peleasService.Delete(id);
        return NoContent();
    }    

}