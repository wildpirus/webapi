using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class RasgosController: ControllerBase {
    IRasgosService rasgosService;

    public RasgosController(IRasgosService service) {
        rasgosService = service;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(rasgosService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Rasgo rasgo) {
        rasgosService.Save(rasgo);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Rasgo rasgo) {
        rasgosService.Update(id, rasgo);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        rasgosService.Delete(id);
        return Ok();
    }    

}