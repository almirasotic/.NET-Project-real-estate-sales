using Microsoft.AspNetCore.Mvc;
using BackInformSistemi.Interfaces;       // Gde je definisan IPregovorRepository
using BackInformSistemi.Models;
using BackInformSistemi.Dtos;

namespace BackInformSistemi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PregovorController : ControllerBase
    {
        private readonly IPregovorRepository _pregovorRepo;

        public PregovorController(IPregovorRepository pregovorRepo)
        {
            _pregovorRepo = pregovorRepo;
        }

        [HttpGet]
        public IActionResult GetAllPregovori()
        {
            var pregovori = _pregovorRepo.GetAllPregovori();
            return Ok(pregovori);
        }

        [HttpGet("{id}")]
        public IActionResult GetPregovor(int id)
        {
            var p = _pregovorRepo.GetPregovorById(id);
            if (p == null)
                return NotFound("Pregovor nije pronađen.");

            return Ok(p);
        }
        // POST /api/Pregovor
        [HttpPost]
        public IActionResult CreatePregovor([FromBody] Pregovor pregovor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _pregovorRepo.CreatePregovor(pregovor);
            if (!success)
            {
                return StatusCode(500, new { message = "Dogodila se greška prilikom čuvanja pregovora." });
            }

            // Umesto običnog teksta, vraćamo JSON
            return Ok(new
            {
                message = "Pregovor uspešno kreiran.",
                data = pregovor // Opcionalno, vraćamo kreirani pregovor
            });
        }

        [HttpGet("property/{propertyId}")]
        public IActionResult GetPregovoriByProperty(int propertyId)
        {
            var pregovori = _pregovorRepo.GetPregovoriByProperty(propertyId);
            if (pregovori == null || !pregovori.Any())
            {
                return NotFound(new { Message = "Nema pregovora za zadatu nekretninu." });
            }

            return Ok(pregovori);
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePregovor(int id, [FromBody] Pregovor pregovor)
        {
            if (pregovor == null || id != pregovor.Id)
            {
                return BadRequest("Invalid data.");
            }

            var existingPregovor = _pregovorRepo.GetPregovorById(id);
            if (existingPregovor == null)
            {
                return NotFound("Pregovor not found.");
            }

            existingPregovor.offer = pregovor.offer;
            existingPregovor.status = pregovor.status;

            var success = _pregovorRepo.UpdatePregovor(existingPregovor);
            if (!success)
            {
                return StatusCode(500, new { message = "An error occurred while updating the pregovor." });
            }

            return Ok(new { message = "Pregovor updated successfully.", pregovor = existingPregovor });
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePregovor(int id)
        {
            var existingPregovor = _pregovorRepo.GetPregovorById(id);
            if (existingPregovor == null)
            {
                return NotFound(new { message = "Pregovor nije pronađen." });
            }

            var success = _pregovorRepo.DeletePregovor(existingPregovor);
            if (!success)
            {
                return StatusCode(500, new { message = "Greška prilikom brisanja pregovora." });
            }

            return Ok(new { message = "Pregovor uspešno obrisan." });
        }







    }
}
