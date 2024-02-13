namespace PetStore.Api.MappingProfile
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<User, DisplayPetsByUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets))
                .ReverseMap();

            CreateMap<User, DisplayPetsByUserDto>().ReverseMap();

            CreateMap<DisplayPetsByUser, DisplayPetsByUserDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Pets.Select(p => p.Images)))
                .ReverseMap();
        }

    }
}
