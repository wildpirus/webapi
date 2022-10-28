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

    [HttpGet]
    public IActionResult Get() {
        return Ok(enfrentamientosService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Enfrentamiento enfrentamiento) {
        enfrentamientosService.Save(enfrentamiento);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Enfrentamiento enfrentamiento) {
        enfrentamientosService.Update(id, enfrentamiento);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        enfrentamientosService.Delete(id);
        return Ok();
    }    

}