using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class PatrocinadoresController: ControllerBase {
    IPatrocinadoresService patrocinadoresService;

    public PatrocinadoresController(IPatrocinadoresService service) {
        patrocinadoresService = service;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(patrocinadoresService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Patrocinador patrocinador) {
        patrocinadoresService.Save(patrocinador);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Patrocinador patrocinador) {
        patrocinadoresService.Update(id, patrocinador);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        patrocinadoresService.Delete(id);
        return Ok();
    }    

}