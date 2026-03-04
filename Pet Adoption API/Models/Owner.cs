namespace Pet_Adoption_API.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<AdoptionApplication> Applications { get; set; } = new List<AdoptionApplication>();
    }
}
