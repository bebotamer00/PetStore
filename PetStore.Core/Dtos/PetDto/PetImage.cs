using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Core.Dtos.PetDto
{
    public class PetImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
