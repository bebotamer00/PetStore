using PetStore.Core.Dtos.PetDto;

namespace PetStore.Core.Dtos.User
{
    public class UserDto
    {
        [DisplayName("First Name"), MaxLength(50, ErrorMessage = ErrorMessages.MaxLength)]
        public string FirstName { get; set; }

        [DisplayName("Last Name"), MaxLength(50, ErrorMessage = ErrorMessages.MaxLength)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        //public IEnumerable<DisplayPets> Pets { get; set; }
    }
}
