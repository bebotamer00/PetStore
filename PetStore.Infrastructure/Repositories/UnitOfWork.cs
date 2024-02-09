using PetStore.Core.Interfaces;
using PetStore.Infrastructure.Data;

namespace PetStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            PetRepository = new PetRepository(_context);
        }

        public IUserRepository UserRepository { get; }
        public IPetRepository PetRepository { get; }
    }
}
