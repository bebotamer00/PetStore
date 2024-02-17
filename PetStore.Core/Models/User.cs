namespace PetStore.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("First Name"), MaxLength(50, ErrorMessage = ErrorMessages.MaxLength)]
        public string FirstName { get; set; }

        [DisplayName("Last Name"), MaxLength(50, ErrorMessage = ErrorMessages.MaxLength)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

        public int? VetId { get; set; }
        public virtual Vet? Vet { get; set; }

        public virtual ICollection<UserVet> UserVets { get; set; } = new HashSet<UserVet>();
    }
}
