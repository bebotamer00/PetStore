using PetStore.Core.Dtos.PetDto;

namespace PetStore.Core.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<DisplayPets>> GetAllAsync();
        Task AddPetWithImage(CreatePetDto createPetDto);
        Task UpdatePetWithImage(UpdatePetDto updatePetDto);
    }
}
