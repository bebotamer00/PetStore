namespace PetStore.Core.Models
{
    public class UserVet
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int VetId { get; set; }
        public Vet Vet { get; set; }
    }
}
