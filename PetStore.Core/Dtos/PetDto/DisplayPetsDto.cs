using System.Text.Json.Serialization;

namespace PetStore.Core.Dtos.PetDto
{
    public class DisplayPets
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [Length(50, 250, ErrorMessage = ErrorMessages.MaxAndMinValidation)]
        public string Description { get; set; }
        public short Age { get; set; }

        [Range(2, 5, ErrorMessage = ErrorMessages.MaxAndMinValidation)]
        public List<PetImageDto> Images { get; set; }
        public double Weight { get; set; }
        public Gender Gender { get; set; }
        public decimal Price { get; set; }
        public string UserName => $"{FirstName} {LastName}";

        [JsonIgnore]
        public string FirstName { get; set; }
        [JsonIgnore]
        public string LastName { get; set; }
    }
}
