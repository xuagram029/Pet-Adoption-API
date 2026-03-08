using Pet_Adoption_API.DTOs.Pet;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Interface
{
    public interface IPetRepository
    {
        public Task<ICollection<Pet>> GetAllPetsAsync();
        public Task<Pet?> GetByIdAsync(int id);
        public Task<Pet> CreateAsync(Pet pet);
        public Task<Pet?> UpdateAsync(int id, PetUpdateDto petDto);
        public Task<Pet?> DeleteAsync(int id);
        public Task<bool> PetExistsAsync(int petId);
        public Task<bool> ShelterExistsAsync(int shelterId);
        public Task<bool> OwnerExistsAsync(int ownerId);
    }
}
