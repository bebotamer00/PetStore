﻿using PetStore.Core.Dtos.PetDto;

namespace PetStore.Api.MappingProfile
{
    public class MappingPets : Profile
    {
        public MappingPets()
        {
            CreateMap<Pet, DisplayPets>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ReverseMap();

            CreateMap<Pet, DisplayPetsByUserDto>()
              .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
              .ReverseMap();

            CreateMap<Pet, CreatePetDto>()
                .ReverseMap();

            CreateMap<Pet, UpdatePetDto>().ReverseMap();

            CreateMap<IFormFile, PetImage>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.FileName))
            .ReverseMap();

            CreateMap<PetImage, PetImageDto>().ReverseMap();
        }
    }
}
