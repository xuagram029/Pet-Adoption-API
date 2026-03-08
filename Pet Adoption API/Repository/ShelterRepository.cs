using Microsoft.EntityFrameworkCore;
using Pet_Adoption_API.Data;
using Pet_Adoption_API.DTOs.Shelter;
using Pet_Adoption_API.Interface;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Repository
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly ApplicationDBContext _context;
        public ShelterRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Shelter> CreateAsync(Shelter shelter)
        {
            await _context.Shelters.AddAsync(shelter);
            await _context.SaveChangesAsync();
            return shelter;
        }

        public async Task<Shelter?> DeleteAsync(int id)
        {
            var existingShelter = await _context.Shelters.FirstOrDefaultAsync(s => s.Id == id);
            if (existingShelter == null)
            {
                return null;
            }
            _context.Shelters.Remove(existingShelter);
            await _context.SaveChangesAsync();
            return existingShelter;
        }

        public async Task<ICollection<Shelter>> GetAllAsync()
        {
            return await _context.Shelters
                .Include(s => s.Pets.Where(p => !p.IsAdopted))
                .ToListAsync();
        }

        public async Task<Shelter?> GetByIdAsync(int id)
        {
            var existingShelter = await _context.Shelters
                .Include(s => s.Pets.Where(p => !p.IsAdopted))
                .FirstOrDefaultAsync(s => s.Id == id);

            if(existingShelter == null)
            {
                return null;
            }

            return existingShelter;
        }

        public async Task<bool> ShelterNameExistsAsync(string shelterName)
        {
            return await _context.Shelters.AnyAsync(s => s.Name.ToLower() == shelterName.ToLower());
        }

        public async Task<Shelter?> UpdateAsync(int id, ShelterUpsertDto shelter)
        {
            var existingShelter = await _context.Shelters.FirstOrDefaultAsync(s => s.Id == id);
            if (existingShelter == null)
            {
                return null;
            }

            existingShelter.Name = shelter.Name;
            existingShelter.Address = shelter.Address;
            existingShelter.PhoneNumber = shelter.PhoneNumber;
            existingShelter.Email = shelter.Email;

            await _context.SaveChangesAsync();
            return existingShelter;
        }
    }
}
