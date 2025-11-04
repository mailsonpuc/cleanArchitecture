
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoop.Domain.Entities;

namespace Shoop.Infrastructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Username).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Password).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Role).HasMaxLength(100).IsRequired();
        }
    }
}