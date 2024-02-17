using PetStore.Core.Dtos.UserDto;

namespace PetStore.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<DisplayPetsByUser>> GetAllAsync(string? searchUserName);
        Task<DisplayPetsByUser> GetUserByEmail(string email);
        Task UpdatePassword(int id, string newPassword);
        Task<IEnumerable<DisplayPetsByUser>> GetPetsByUserAsync(int userId);
        Task<IEnumerable<DisplayPetsByUser>> GetPetsByUserNameAsync(string userName);
    }
}
