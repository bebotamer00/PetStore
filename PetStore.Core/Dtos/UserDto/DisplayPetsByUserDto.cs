using PetStore.Core.Dtos.PetDto;

namespace PetStore.Core.Dtos.UserDto
{
    public class DisplayPetsByUserDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Length(50, 250, ErrorMessage = ErrorMessages.MaxAndMinValidation)]
        public string Description { get; set; }
        public short Age { get; set; }
        public List<PetImageDto> Images { get; set; }
        public double Weight { get; set; }
        public Gender Gender { get; set; }
        public decimal Price { get; set; }
    }
}
