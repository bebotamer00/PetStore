namespace PetStore.Api.MappingProfile
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pets))
                .ReverseMap();
        }

    }
}
