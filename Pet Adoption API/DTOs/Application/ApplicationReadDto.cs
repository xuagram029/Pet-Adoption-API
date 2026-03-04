namespace Pet_Adoption_API.DTOs.Application
{
    public class ApplicationReadDto
    {
        public int Id { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public string AdopterName { get; set; } = string.Empty;
    }
}
