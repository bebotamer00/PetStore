using PetStore.Core.Dtos.AddressDto;

namespace PetStore.Core.Dtos.VetDto
{
    public class UpdateVetDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAvailable { get; set; }
        public DisplayAddressDto Address { get; set; }

        public ICollection<WorkingHoursDto> WorkingHours { get; set; }
    }
}
