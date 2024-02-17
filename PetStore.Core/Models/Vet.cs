namespace PetStore.Core.Models
{
    public class Vet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAvailable { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public virtual ICollection<UserVet> UserVets { get; set; } = [];
        public virtual ICollection<WorkingHours> WorkingHours { get; set; } = [];
        public virtual ICollection<Feedback> Feedbacks { get; set; } = [];
    }
}
