using APBD4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD4.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        // GET: api/animals
        [HttpGet]
        public IActionResult GetAnimals([FromQuery] string name)
        {
            var animals = Animal.GetExtent();
            // if name parameter is not null or empty, filter animals by name
            if (!string.IsNullOrWhiteSpace(name))
            {
                animals = animals.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                
                if (animals.Count == 0)
                {
                    return NotFound(new { error = $"No animals found with name containing '{name}'." });
                }
            }
            return Ok(animals);
        }
        // POST: api/animals adding animal
        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal animal)
        {
            try
            {
                Animal.AddToExtent(animal);
                return Ok($"Animal added successfully! The id of the animal is: {animal.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        // GET: api/animals/{id} getting animal by id
        [HttpGet("{id}")]
        public IActionResult GetAnimalById(int id)
        {
            var animal = Animal.FindAnimalById(id);
            if (animal == null)
            {
                return NotFound(new { error = $"Animal with id {id} not found." });
            }
            return Ok(animal);
        }
        // PUT: api/animals/{id} editing animal by id
        [HttpPut("{id}")]
        public IActionResult EditAnimal(int id, [FromBody] Animal animal)
        {
            var OgAnimal = Animal.FindAnimalById(id);
            try
            {
                OgAnimal.EditAnimal(animal);
                return Ok($"Animal edited successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        // DELETE: api/animals/{id} removing animal by id
        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = Animal.FindAnimalById(id);
            if (animal == null)
            {
                return NotFound(new { error = $"Animal with id {id} not found." });
            }
            Animal.RemoveFromExtent(animal);
            return Ok("Animal deleted successfully!");
        }
        // // GET: api/animals/search?name={name} searching animal by name
        // [HttpGet("search")]
        // public IActionResult SearchAnimalByName([FromQuery] string name)
        // {
        //     var animals = Animal.GetExtent().Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        //     
        //     if (animals.Count == 0)
        //     {
        //         return NotFound(new { error = $"No animals found with name containing '{name}'." });
        //     }
        //     return Ok(animals);
        // }
    }
}
