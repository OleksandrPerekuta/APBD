using Microsoft.AspNetCore.Mvc;
using WebApplication1.Classes;
using WebApplication1.Interfaces;

namespace WebApplication1;


[ApiController]
[Route("animals")]
public class AnimalController : ControllerBase
{
    private IAnimalDb _animalDb;

    public AnimalController(IAnimalDb animalDb)
    {
        _animalDb = animalDb;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_animalDb.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var animal = _animalDb.GetById(id);
        if (animal is null) return NotFound();
        return Ok(animal);
    }

    [HttpGet("{id}/appointment")]
    public IActionResult GetAppointments(int id)
    {
        var animal = _animalDb.GetById(id);
        if (animal is null) return NotFound();
        return Ok(animal.GetAppointments());
    }

    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        _animalDb.AddAnimal(animal);
        return Created();
    }

    [HttpPost("{id}/appointments")]
    public IActionResult AddAppointment(int id, Appointment appointment)
    {
        var animal = _animalDb.GetById(id);
        if (animal is null) return NotFound();
        animal.AddAppointment(appointment);
        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult PutAnimal(int id, Animal animal)
    {
        var isUpdated = _animalDb.UpdateAnimal(animal, id);
        if (isUpdated) return NoContent();
        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveAnimal(int id)
    {
        var isRemoved = _animalDb.RemoveAnimalById(id);
        if (isRemoved) return NoContent();
        return NotFound();
    }
    
}