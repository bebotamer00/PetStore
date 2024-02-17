using System.Text.Json.Serialization;

namespace PetStore.Core.Dtos.FeedbackDto
{
    public class DisplayFeedbackDto
    {
        public string UserName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
