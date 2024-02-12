using Microsoft.AspNetCore.Http;
using PetStore.Core.Dtos.PetDto;

namespace PetStore.Core.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<DisplayPets>> GetAllAsync();
        Task AddPetWithImage(CreatePetDto createPetDto, List<IFormFile> images);
        Task UpdatePetWithImage(UpdatePetDto updatePetDto, List<IFormFile> newImages);
        Task<bool> DeleteAsyncWithImage(int id);
        Task<IEnumerable<Pet>> SearchPetsAsync(string searchTerm);
    }
}
