using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Core.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Length(50, 250, ErrorMessage = ErrorMessages.MaxAndMinValidation)]
        public string Description { get; set; }
        public short Age { get; set; }
        public string Image { get; set; }
        public double Weight { get; set; }
        public Gender Gender { get; set; }
        public decimal Price { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
