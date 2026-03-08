using Pet_Adoption_API.DTOs.Shelter;
using Pet_Adoption_API.Models;

namespace Pet_Adoption_API.Interface
{
    public interface IShelterRepository
    {
        Task<ICollection<Shelter>> GetAllAsync();
        Task<Shelter?> GetByIdAsync(int id);
        Task<Shelter> CreateAsync(Shelter shelter);
        Task<Shelter?> UpdateAsync(int id, ShelterUpsertDto shelter);
        Task<Shelter?> DeleteAsync(int id);
        Task<bool> ShelterNameExistsAsync(string shelterName);
    }
}
