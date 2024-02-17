using PetStore.Core.Dtos.FeedbackDto;

namespace PetStore.Core.Dtos.VetDto
{
    public class DisplayVetWithFeedbackDto
    {
        public string Name { get; set; }

        public ICollection<DisplayFeedbackDto> Feedbacks { get; set; } = [];
    }
}
