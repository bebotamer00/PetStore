using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Infrastructure.Data.Config
{
    public class UserVetConfig : IEntityTypeConfiguration<UserVet>
    {
        public void Configure(EntityTypeBuilder<UserVet> builder)
        {
            builder.HasKey(uv => new { uv.UserId, uv.VetId });

            builder.HasOne(uv => uv.User)
                   .WithMany(u => u.UserVets)
                   .HasForeignKey(uv => uv.UserId);

            builder.HasOne(uv => uv.Vet)
                   .WithMany(v => v.UserVets)
                   .HasForeignKey(uv => uv.VetId);
        }
    }
}
