namespace PetStore.Api.MappingProfile
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<User, UserDto>()
                /*.ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets))*/
                .ReverseMap();

            CreateMap<User, DisplayPetsByUser>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
           .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets))
           .ReverseMap();
        }

    }
}
