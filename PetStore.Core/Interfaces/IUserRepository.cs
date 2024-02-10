using PetStore.Core.Dtos.User;

namespace PetStore.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> SearchByName(string searchTerm);
        Task UpdatePassword(int id, string newPassword);
    }
}
