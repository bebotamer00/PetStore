using AutoMapper;

namespace PetStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            UserRepository = new UserRepository(_context, _mapper);
            PetRepository = new PetRepository(_context, _mapper);
            VetRepository = new VetRepository(_context, _mapper);
        }

        public IUserRepository UserRepository { get; }
        public IPetRepository PetRepository { get; }
        public IVetRepository VetRepository { get; }
    }
}
