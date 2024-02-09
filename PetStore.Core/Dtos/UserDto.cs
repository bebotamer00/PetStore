using PetStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Core.Dtos
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
        public virtual ICollection<Pet>? Pets { get; set; } = new HashSet<Pet>();
    }
}
