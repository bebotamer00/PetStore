namespace PetStore.Core.Models
{
    public class PetImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
