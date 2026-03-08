using Microsoft.AspNetCore.Mvc;
using Pet_Adoption_API.DTOs.Pet;
using Pet_Adoption_API.Interface;
using Pet_Adoption_API.Mappers;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _petRepo;
        public PetController(IPetRepository petRepo)
        {
            _petRepo = petRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _petRepo.GetAllPetsAsync();
            var petDtos = pets.Select(p => p.ToReadDto());
            return Ok(petDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var pet = await _petRepo.GetByIdAsync(id);
            if(pet == null)
            {
                return NotFound();
            }
            return Ok(pet.ToReadDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PetCreateDto petDto)
        {
            if(petDto.ShelterId.HasValue && petDto.OwnerId.HasValue)
            {
                return BadRequest("A pet cannot be in a shelter and owned by someone at the same time.");
            }

            // Existence Check for Shelter
            if (petDto.ShelterId.HasValue)
            {
                if (!await _petRepo.ShelterExistsAsync(petDto.ShelterId.Value))
                    return NotFound("Shelter not found.");
            }
            // Existence Check for Owner
            if (petDto.OwnerId.HasValue)
            {
                if (!await _petRepo.OwnerExistsAsync(petDto.OwnerId.Value))
                return NotFound("Owner not found.");
            }

            var petModel = petDto.ToEntity();
            await _petRepo.CreateAsync(petModel);
            return CreatedAtAction(nameof(GetById), new { id = petModel.Id }, petModel.ToReadDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PetUpdateDto updateDto)
        {
            if (updateDto.ShelterId == null && updateDto.OwnerId == null)
            {
                return BadRequest("A pet must belong to either a Shelter or an Owner.");
            }

            if (updateDto.ShelterId.HasValue)
            {
                if (!await _petRepo.ShelterExistsAsync(updateDto.ShelterId.Value))
                    return NotFound("The specified Shelter does not exist.");
            }

            if (updateDto.OwnerId.HasValue)
            {
                if (!await _petRepo.OwnerExistsAsync(updateDto.OwnerId.Value))
                    return NotFound("The specified Owner does not exist.");
            }

            var petModel = await _petRepo.UpdateAsync(id, updateDto);

            if (petModel == null)
            {
                return NotFound("Pet not found.");
            }

            return Ok(petModel.ToReadDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var petModel = await _petRepo.DeleteAsync(id);

            if (petModel == null) return NotFound("Pet does not exist");

            return NoContent(); 
        }
    }
}
