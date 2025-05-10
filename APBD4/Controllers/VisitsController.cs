using System.Globalization;
using APBD4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD4.Controllers

{
    [Route("api/animals/{animalId}/visits")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        // GET: api/animals/{animalId}/visits getting visits by animal id
        [HttpGet]
        public IActionResult GetVisits(int animalId)
        {
            var animal = Animal.FindAnimalById(animalId);
            if (animal == null)
                return NotFound(new { error = $"Animal with id {animalId} not found." });

            var visits = Visit.FindByAnimalId(animalId);

            return Ok(visits);
        }
        // POST: api/animals/{animalId}/visits adding visit
        [HttpPost]
        public IActionResult AddVisit([FromBody] VisitSomething visitTemplate)
        {
            var animal = Animal.FindAnimalById(visitTemplate.AnimalId);
            if (animal == null)
                return NotFound(new { error = $"Animal with id {visitTemplate.AnimalId} not found." });
            
            // parse date in non american format
            if (!DateTime.TryParseExact(visitTemplate.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                return BadRequest(new { error = $"Date is not in correct format." });
            try
            {
                var visit = new Visit(date,animal, visitTemplate.Description, visitTemplate.Price);
                Visit.AddToExtent(visit);
                return Ok("The visit has been added successfully!");
            }catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            
        }
    }
}
