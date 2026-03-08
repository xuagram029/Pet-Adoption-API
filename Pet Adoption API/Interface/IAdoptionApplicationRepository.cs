using Pet_Adoption_API.DTOs.Application;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Interface
{
    public interface IAdoptionApplicationRepository
    {
        Task<ICollection<AdoptionApplication>> GetAllAsync();
        Task<AdoptionApplication?> GetByIdAsync(int id);
        Task<AdoptionApplication> CreateAsync(AdoptionApplication application);
        Task<AdoptionApplication?> UpdateAsync(int id, ApplicationUpsertDto application);
        Task<bool> DeleteAsync(int id);
    }
}
