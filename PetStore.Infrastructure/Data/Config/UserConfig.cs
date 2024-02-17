using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Infrastructure.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
        }
    }
}
