using Microsoft.EntityFrameworkCore;
using Pet_Adoption_API.Data;
using Pet_Adoption_API.DTOs.Pet;
using Pet_Adoption_API.Interface;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDBContext _context;
        public PetRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Pet> CreateAsync(Pet petModel)
        {
            await _context.Pets.AddAsync(petModel);
            await _context.SaveChangesAsync();
            return petModel;
        }

        public async Task<Pet?> DeleteAsync(int id)
        {
            var petModel = await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);

            if (petModel == null) return null;

            _context.Pets.Remove(petModel);
            await _context.SaveChangesAsync();
            return petModel;
        }

        public async Task<ICollection<Pet>> GetAllPetsAsync()
        {
            return await _context.Pets.Include(p => p.Shelter).ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(int id)
        {
            var pet = await _context.Pets.Include(p => p.Shelter).FirstOrDefaultAsync(p => p.Id == id);
            if(pet == null)
            {
                return null;
            }
            return pet;
        }

        public async Task<bool> OwnerExistsAsync(int ownerId)
        {
            return await _context.Owners.AnyAsync(o => o.Id == ownerId);
        }

        public Task<bool> PetExistsAsync(int petId)
        {
            return _context.Pets.AnyAsync(p => p.Id == petId);
        }

        public Task<bool> ShelterExistsAsync(int shelterId)
        {
            return _context.Shelters.AnyAsync(s => s.Id == shelterId);
        }

        public async Task<Pet?> UpdateAsync(int id, PetUpdateDto petDto)
        {
            var existingPet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPet == null)
            {
                return null;
            }

            existingPet.Name = petDto.Name;
            existingPet.Species = petDto.Species;
            existingPet.Breed = petDto.Breed;
            existingPet.Age = petDto.Age;
            existingPet.Description = petDto.Description;
            existingPet.IsAdopted = petDto.IsAdopted;
            existingPet.ShelterId = petDto.ShelterId;
            existingPet.OwnerId = petDto.OwnerId;

            await _context.SaveChangesAsync();
            return existingPet;
        }
    }
}
