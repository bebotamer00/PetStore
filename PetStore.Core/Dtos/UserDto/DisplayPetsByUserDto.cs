using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetStore.Core.Dtos.UserDto
{
    public class DisplayPetsByUserDto
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [Length(50, 250, ErrorMessage = ErrorMessages.MaxAndMinValidation)]
        public string Description { get; set; }
        public short Age { get; set; }
        public string Image { get; set; }
        public double Weight { get; set; }
        public Gender Gender { get; set; }
        public decimal Price { get; set; }
    }
}
