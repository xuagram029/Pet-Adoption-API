namespace Pet_Adoption_API.DTOs.Pet
{
    public class PetCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsAdopted { get; set; } = false;
        public int? ShelterId { get; set; }
        public int? OwnerId { get; set; }
    }
}
