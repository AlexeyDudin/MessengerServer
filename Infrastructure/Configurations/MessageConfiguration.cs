using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.From).WithMany(u => u.OutputMessages).HasForeignKey(u => u.FromUserId);
            builder.HasOne(m => m.ToUser).WithMany(u => u.InputMessages).HasForeignKey(u => u.ToUserId);
            builder.HasOne(m => m.ToGroup).WithMany(g => g.Messages).HasForeignKey(u => u.ToGroupId);
        }
    }
}
