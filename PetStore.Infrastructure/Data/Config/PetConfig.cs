using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Infrastructure.Data.Config
{
    public class PetConfig : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
        }
    }
}
