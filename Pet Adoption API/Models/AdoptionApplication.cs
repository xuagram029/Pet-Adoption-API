namespace Pet_Adoption_API.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        // Foreign Keys
        public int PetId { get; set; }
        public Pet Pet { get; set; } = null!;

        public int OwnerId { get; set; }
        public Owner Owner { get; set; } = null!;
    }
}
