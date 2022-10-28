using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
public class SupersController: ControllerBase {
    ISupersService supersService;
    SupersContext dbcontext;

    public SupersController(ISupersService service, SupersContext db) {
        supersService = service;
        dbcontext = db;
    }

    [HttpGet(Name = "GetHero")]
    //[Route("1")]
    //[Route("2")]
    //[Route("[action]")]
    public IActionResult Get() {
        return Ok(supersService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Super super) {
        supersService.Save(super);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Super super) {
        supersService.Update(id, super);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) {
        supersService.Delete(id);
        return Ok();
    }   

    [HttpGet]
    [Route("createdb")]
    public IActionResult CreateDatabase()
    {
        dbcontext.Database.EnsureCreated();

        return Ok();
    }

}