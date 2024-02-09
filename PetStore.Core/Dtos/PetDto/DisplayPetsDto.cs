using PetStore.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Core.Dtos.PetDto
{
    public class DisplayPets
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [Length(50, 250, ErrorMessage = ErrorMessages.MaxAndMinValidation)]
        public string Description { get; set; }
        public short Age { get; set; }
        public IFormFile Image { get; set; }
        public double Weight { get; set; }
        public Gender Gender { get; set; }
        public decimal Price { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
