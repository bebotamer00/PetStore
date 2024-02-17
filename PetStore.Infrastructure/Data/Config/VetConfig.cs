using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Infrastructure.Data.Config
{
    public class VetConfig : IEntityTypeConfiguration<Vet>
    {
        public void Configure(EntityTypeBuilder<Vet> builder)
        {
            builder.HasOne(v => v.User)
                   .WithOne(u => u.Vet)
                   .HasForeignKey<Vet>(v => v.UserId);

            builder.HasMany(v => v.WorkingHours)
                   .WithOne()
                   .HasForeignKey(wh => wh.VetId);
        }
    }
}
