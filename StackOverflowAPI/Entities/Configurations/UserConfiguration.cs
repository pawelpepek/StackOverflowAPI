using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowAPI.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        builder.HasMany(u => u.LikedPosts).WithMany(p => p.UserLikes);
        builder.HasMany(u => u.DislikedPosts).WithMany(p => p.UserDislikes);
    }
}
