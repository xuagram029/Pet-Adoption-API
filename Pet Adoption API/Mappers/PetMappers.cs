using Pet_Adoption_API.DTOs.Pet;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Mappers
{
    public static class PetMappers
    {
        public static PetReadDto ToReadDto(this Pet pet)
        {
            return new PetReadDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Species = pet.Species,
                Breed = pet.Breed,
                Age = pet.Age,
                Description = pet.Description,
                IsAdopted = pet.IsAdopted,

                ShelterId = pet.ShelterId,
                ShelterName = pet.Shelter?.Name,

                OwnerId = pet.OwnerId,
                OwnerName = pet.Owner != null ? $"{pet.Owner.FirstName} {pet.Owner.LastName}" : null
            };
        }

        public static Pet ToEntity(this PetCreateDto dto)
        {
            return new Pet
            {
                Name = dto.Name,
                Species = dto.Species,
                Breed = dto.Breed,
                Age = dto.Age,
                Description = dto.Description,
                IsAdopted = dto.IsAdopted,
                ShelterId = dto.ShelterId,
                OwnerId = dto.OwnerId
            };
        }
    }
}
