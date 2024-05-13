using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IAnimalsService _animalsService;

    public WarehouseController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }
    
    // Endpoint used to return list of animals.
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "")
    {
        var animals = _animalsService.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        var affectedCount = _animalsService.CreateAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{idAnimal:int}")]
    public IActionResult UpdateAnimal(int idAnimal, Animal animal)
    {
        var affectedCount = _animalsService.UpdateAnimal(idAnimal, animal);
        return NoContent();
    }

    [HttpDelete("{idAnimal:int}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        var affectedCount = _animalsService.DeleteAnimal(idAnimal);
        return NoContent();
    }
}