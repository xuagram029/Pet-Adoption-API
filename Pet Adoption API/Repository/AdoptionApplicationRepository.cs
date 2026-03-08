using Microsoft.EntityFrameworkCore;
using Pet_Adoption_API.Data;
using Pet_Adoption_API.DTOs.Application;
using Pet_Adoption_API.Interface;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Repository
{
    public class AdoptionApplicationRepository : IAdoptionApplicationRepository
    {
        private readonly ApplicationDBContext _context;
        public AdoptionApplicationRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<AdoptionApplication> CreateAsync(AdoptionApplication application)
        {
            await _context.AdoptionApplications.AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingApplication = await _context.AdoptionApplications.FirstOrDefaultAsync(a => a.Id == id);
            if(existingApplication == null)
            {
                return false;
            }
            existingApplication.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<AdoptionApplication>> GetAllAsync()
        {
            return await _context.AdoptionApplications
                // To ignore the global query filter
                //.IgnoreQueryFilters()
                .Include(a => a.Pet)
                .Include(a => a.Owner)
                .ToListAsync();
        }

        public async Task<AdoptionApplication?> GetByIdAsync(int id)
        {
            var existingApplication = await _context.AdoptionApplications
                .Include(a => a.Pet)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(a => a.Id == id);

            if(existingApplication == null)
            {
                return null;
            }
            return existingApplication;
        }

        public async Task<AdoptionApplication?> UpdateAsync(int id, ApplicationUpsertDto application)
        {
            var existingApplication = await _context.AdoptionApplications
                .FirstOrDefaultAsync(a => a.Id == id);
            if(existingApplication == null)
            {
                return null;
            }

            existingApplication.PetId = application.PetId;
            existingApplication.OwnerId = application.OwnerId;
            await _context.SaveChangesAsync();
            return existingApplication;
        }
    }
}
