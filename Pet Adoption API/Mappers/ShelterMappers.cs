using Pet_Adoption_API.DTOs.Pet;
using Pet_Adoption_API.DTOs.Shelter;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Mappers
{
    public static class ShelterMappers
    {
        public static ShelterReadDto ToShelterDto(this Shelter shelter)
        {
            return new ShelterReadDto
            {
                Id = shelter.Id,
                Name = shelter.Name,
                Address = shelter.Address,
                PhoneNumber = shelter.PhoneNumber,
                Email = shelter.Email,
                Pets = shelter.Pets?.Select(p => p.ToReadDto()).ToList() ?? new List<PetReadDto>()
            };
        }

        public static Shelter ToEntity(this ShelterUpsertDto shelter)
        {
            return new Shelter
            {
                Name = shelter.Name,
                Address = shelter.Address,
                PhoneNumber = shelter.PhoneNumber,
                Email = shelter.Email,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
