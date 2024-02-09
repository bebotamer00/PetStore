using Microsoft.EntityFrameworkCore;
using PetStore.Core.Models;

namespace PetStore.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }
}
