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
        public List<PetImage> Images { get; set; } = [];
        public double Weight { get; set; }
        public Gender Gender { get; set; }
        public decimal Price { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female
    }
}
