using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowAPI.Entities.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.Content).IsRequired();
        builder.Property(m => m.Created).HasDefaultValueSql("getutcdate()");
        builder.Property(m => m.Edited).ValueGeneratedOnUpdate();
    }
}