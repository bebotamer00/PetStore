using PetStore.Core.Interfaces;
using PetStore.Core.Models;
using PetStore.Infrastructure.Data;

namespace PetStore.Infrastructure.Repositories
{
    public class PetRepository(ApplicationDbContext context) : Repository<Pet>(context), IPetRepository
    {
    }
}
