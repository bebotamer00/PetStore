namespace PetStore.Core.Dtos.UserDto
{
    public class DisplayPetsByUser
    {
        public string UserName { get; set; }
        public IEnumerable<DisplayPetsByUserDto> Pets { get; set; }
    }
}
