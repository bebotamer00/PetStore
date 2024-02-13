using AutoMapper;
using PetStore.Core.Dtos.UserDto;

namespace PetStore.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context, IMapper mapper) : Repository<User>(context), IUserRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<DisplayPetsByUser>> GetAllAsync(string? searchUserName)
        {
            IQueryable<User> usersQuery = _context.Users
                .Include(u => u.Pets)
                .ThenInclude(p => p.Images)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(searchUserName))
                usersQuery = usersQuery.Where(u => u.FirstName.Contains(searchUserName) || u.LastName.Contains(searchUserName));


            var getAllUsers = await usersQuery.ToListAsync();

            var result = _mapper.Map<IEnumerable<DisplayPetsByUser>>(getAllUsers);

            return result;
        }

        public async Task<DisplayPetsByUser> GetUserByEmail(string email)
        {
            ArgumentNullException.ThrowIfNull(email);

            var getUserByEmail = await _context.Users
                .Include(u => u.Pets)
                .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(u => u.Email == email);

            var result = _mapper.Map<DisplayPetsByUser>(getUserByEmail);

            return result;
        }

        public async Task UpdatePassword(int id, string newPassword)
        {
            if (id <= 0)
                throw new ArgumentException("Id Not Found");

            if (newPassword is null)
                throw new ArgumentException("Password Is Null");

            var user = await _context.Users.FindAsync(id) ?? throw new ArgumentException("User is null");

            user.Password = newPassword;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DisplayPetsByUser>> GetPetsByUserAsync(int userId)
        {
            var userPets = await _context.Users
                .Include(u => u.Pets)
                .ThenInclude(p => p.Images)
                .Where(u => u.Id == userId)
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<DisplayPetsByUser>>(userPets);

            return result;
        }

        public async Task<IEnumerable<DisplayPetsByUser>> GetPetsByUserNameAsync(string userName)
        {
            if (userName is null)
                throw new ArgumentException("This Name Not Found.");

            var pets = await _context.Users
                .Include(u => u.Pets)
                .Where(u => (u.FirstName + " " + u.LastName).Contains(userName))
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<DisplayPetsByUser>>(pets);

            return result;
        }
    }
}
