using PetStore.Core.Dtos.FeedbackDto;

namespace PetStore.Api.MappingProfile
{
    public class MappingFeedback : Profile
    {
        public MappingFeedback()
        {
            CreateMap<Feedback, DisplayFeedbackDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User!.FirstName} {src.User.LastName}"))
                .ReverseMap();

            CreateMap<Feedback, AddFeedbackDto>().ReverseMap();
        }
    }
}
