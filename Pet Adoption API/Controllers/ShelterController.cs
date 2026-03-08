using Microsoft.AspNetCore.Mvc;
using Pet_Adoption_API.DTOs.Shelter;
using Pet_Adoption_API.Interface;
using Pet_Adoption_API.Mappers;

namespace Pet_Adoption_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterRepository _shelterRepo;
        public ShelterController(IShelterRepository shelterRepo)
        {
            _shelterRepo = shelterRepo;
        }

        [HttpGet]   
        public async Task<IActionResult> GetAll()
        {
            var shelters = await _shelterRepo.GetAllAsync();
            var sheltersToDto = shelters.Select(s => s.ToShelterDto()).ToList();
            return Ok(sheltersToDto);
        }

        [HttpGet("{id}")]   
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var shelter = await _shelterRepo.GetByIdAsync(id);
            if(shelter == null)
            {
                return NotFound();
            }
            return Ok(shelter.ToShelterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShelterUpsertDto shelterModel)
        {
            if(await _shelterRepo.ShelterNameExistsAsync(shelterModel.Name))
            {
                return BadRequest("A shelter with this name already exists.");
            }

            var shelterToCreate = shelterModel.ToEntity();
            var createdShelter = await _shelterRepo.CreateAsync(shelterToCreate);

            return CreatedAtAction(nameof(GetById), new { id = createdShelter.Id }, createdShelter.ToShelterDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ShelterUpsertDto shelterModel)
        {
            var shelterToUpdate = await _shelterRepo.UpdateAsync(id, shelterModel);

            if(shelterToUpdate == null)
            {
                return NotFound("Shelter does not exist");
            }

            return Ok(shelterToUpdate.ToShelterDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var shelterToDelete = await _shelterRepo.DeleteAsync(id);

            if(shelterToDelete == null)
            {
                return NotFound("Shelter does not exist");
            }

            return NoContent();
        }
    }
}
