using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class RasgosSupersController: ControllerBase {
    IRasgosSupersService rasgosSupersService;

    public RasgosSupersController(IRasgosSupersService service) {
        rasgosSupersService = service;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(rasgosSupersService.Get());
    }

    [HttpPost]
    public IActionResult Post([FromBody] RasgoSuper rasgoSuper) {
        rasgosSupersService.Save(rasgoSuper);
        if (rasgoSuper.rasgo_super_id == Guid.Empty){
            return Conflict();
        }else {
            return Created(new Uri("https://www.google.com/"),rasgoSuper);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] RasgoSuper rasgoSuper) {
        rasgosSupersService.Update(id, rasgoSuper);
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        rasgosSupersService.Delete(id);
        return NoContent();
    }    

}