using System.ComponentModel.DataAnnotations;

namespace Pet_Adoption_API.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public bool IsDeleted { get; set; } = false;

        // Foreign Keys
        [Required]
        public int PetId { get; set; }
        public Pet? Pet { get; set; }

        [Required]
        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }
    }
}
