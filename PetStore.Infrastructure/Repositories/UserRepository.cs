using PetStore.Core.Interfaces;
using PetStore.Core.Models;
using PetStore.Infrastructure.Data;

namespace PetStore.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
    {
    }
}
