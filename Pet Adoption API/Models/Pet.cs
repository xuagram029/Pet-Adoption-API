namespace Pet_Adoption_API.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty; // e.g., Dog, Cat
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsAdopted { get; set; } = false;
        public int? ShelterId { get; set; }
        public Shelter? Shelter { get; set; }

        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }
    }
}
