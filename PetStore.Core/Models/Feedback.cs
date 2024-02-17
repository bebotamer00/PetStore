namespace PetStore.Core.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
