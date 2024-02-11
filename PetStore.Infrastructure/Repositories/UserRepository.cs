﻿using AutoMapper;
using PetStore.Core.Dtos.UserDto;

namespace PetStore.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context, IMapper mapper) : Repository<User>(context), IUserRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var getAllUsers = await _context.Users
                .Include(u => u.Pets)
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<UserDto>>(getAllUsers);

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            ArgumentNullException.ThrowIfNull(email);

            var getUserByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return getUserByEmail!;
        }

        public async Task<IEnumerable<User>> SearchByName(string searchTerm) => await _context.Users
           .Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm))
           .ToListAsync();

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
