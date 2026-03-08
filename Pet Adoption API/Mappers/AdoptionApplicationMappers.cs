using Pet_Adoption_API.DTOs.Application;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Mappers
{
    public static class AdoptionApplicationMappers
    {
        public static ApplicationReadDto ToAdoptionApplicationDto(this AdoptionApplication adoptionApplication)
        {
            return new ApplicationReadDto
            {
                Id = adoptionApplication.Id,
                ApplicationDate = adoptionApplication.ApplicationDate,
                Status = adoptionApplication.Status,
                PetName = adoptionApplication.Pet?.Name,
                AdopterName = adoptionApplication.Owner?.FirstName + " " + adoptionApplication.Owner?.LastName
            };
        }

        public static AdoptionApplication ToEntity(this ApplicationUpsertDto applicationDto)
        {
            return new AdoptionApplication
            {
                ApplicationDate = DateTime.UtcNow,
                Status = "Pending",
                PetId = applicationDto.PetId,
                OwnerId = applicationDto.OwnerId
            };
        }
    }
}
