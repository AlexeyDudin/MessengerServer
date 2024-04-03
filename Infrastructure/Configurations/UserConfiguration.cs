using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.HasMany(u => u.Roles).WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
                r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId));
        }
    }
}
