using PetStore.Core.Dtos.AddressDto;
using PetStore.Core.Dtos.FeedbackDto;
using PetStore.Core.Dtos.VetDto;

namespace PetStore.Core.Interfaces
{
    public interface IVetRepository : IRepository<Vet>
    {
        Task<IEnumerable<DisplayVetDto>> GetAllAsync();
        Task AddWorkingHours(Vet vet, WorkingHours workingHours);
        Task<DisplayVetWithFeedbackDto> AddFeedback(AddFeedbackDto feedbackDto);
        Task<DisplayVetWithFeedbackDto> GetVetWithFeedbackAsync(int vetId);
    }
}