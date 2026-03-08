using Microsoft.AspNetCore.Mvc;
using Pet_Adoption_API.DTOs.Application;
using Pet_Adoption_API.Interface;
using Pet_Adoption_API.Mappers;

namespace Pet_Adoption_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionApplicationController : ControllerBase
    {
        private readonly IAdoptionApplicationRepository _adoptionApplicationRepo;
        private readonly IPetRepository _petRepo;
        public AdoptionApplicationController(IAdoptionApplicationRepository adoptionApplicationRepo, IPetRepository petRepo)
        {
            _adoptionApplicationRepo = adoptionApplicationRepo;
            _petRepo = petRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _adoptionApplicationRepo.GetAllAsync();
            var applicationsDto = applications.Select(a => a.ToAdoptionApplicationDto());
            return Ok(applicationsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var application = await _adoptionApplicationRepo.GetByIdAsync(id);
            if(application == null)
            {
                return NotFound();
            }
            return Ok(application.ToAdoptionApplicationDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicationUpsertDto applicationModel)
        {
            if(!await _petRepo.OwnerExistsAsync(applicationModel.OwnerId))
            {
                return BadRequest("Owner does not exist.");
            }

            if(!await _petRepo.PetExistsAsync(applicationModel.PetId))
            {
                return BadRequest("Pet does not exist.");
            }

            var application = applicationModel.ToEntity();
            await _adoptionApplicationRepo.CreateAsync(application);
            var createdApplication = await _adoptionApplicationRepo.GetByIdAsync(application.Id);
            return CreatedAtAction(nameof(GetById), new { id = application.Id }, createdApplication!.ToAdoptionApplicationDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ApplicationUpsertDto applicationModel, [FromRoute] int id)
        {
            if(!await _petRepo.OwnerExistsAsync(applicationModel.OwnerId))
            {
                return BadRequest("Owner does not exist.");
            }

            if(!await _petRepo.PetExistsAsync(applicationModel.PetId))
            {
                return BadRequest("Pet does not exist.");
            }

            var applicationToUpdate = await _adoptionApplicationRepo.UpdateAsync(id, applicationModel);
            if(applicationToUpdate == null)
            {
                return NotFound();
            }


            return Ok("Updated Successfuly");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _adoptionApplicationRepo.DeleteAsync(id);
            if(!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
