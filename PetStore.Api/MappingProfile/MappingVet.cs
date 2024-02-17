using PetStore.Core.Dtos.AddressDto;
using PetStore.Core.Dtos.VetDto;

namespace PetStore.Api.MappingProfile
{
    public class MappingVet : Profile
    {
        public MappingVet()
        {
            CreateMap<Address, DisplayAddressDto>().ReverseMap();


            CreateMap<Vet, DisplayVetDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
                .ReverseMap();

            CreateMap<WorkingHours, WorkingHoursDto>()
                .ReverseMap();

            CreateMap<Vet, AddVetDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
                .ReverseMap();

            CreateMap<Vet, UpdateVetDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
                .ReverseMap();

            CreateMap<Vet, DisplayVetWithFeedbackDto>().ReverseMap();
        }
    }
}