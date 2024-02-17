using PetStore.Core.Dtos.AddressDto;

namespace PetStore.Core.Dtos.VetDto
{
    public class DisplayVetDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAvailable { get; set; }
        public DisplayAddressDto Address { get; set; }

        public List<WorkingHoursDto> WorkingHours { get; set; }
    }
}
