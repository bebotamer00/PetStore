using Microsoft.AspNetCore.Http;
using PetStore.Core.Dtos.PetDto;

namespace PetStore.Core.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<DisplayPets>> GetAllAsync(int? pageNumber, int? pageSize, string? searchName, Gender? gender);
        Task AddPetWithImage(CreatePetDto createPetDto, List<IFormFile> images);
        Task UpdatePetWithImage(UpdatePetDto updatePetDto, List<IFormFile> newImages);
        Task<bool> DeleteAsyncWithImage(int id);
    }
}
