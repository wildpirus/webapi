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
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] RasgoSuper rasgoSuper) {
        rasgosSupersService.Update(id, rasgoSuper);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        rasgosSupersService.Delete(id);
        return Ok();
    }    

}