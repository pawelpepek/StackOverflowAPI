using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowAPI.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        builder.HasMany(u => u.LikedPosts)
               .WithMany(p => p.UserLikes)
               .UsingEntity<Dictionary<string, object>>(
            "UsersLikedPosts",
            j => j.HasOne<Post>().WithMany().OnDelete(DeleteBehavior.ClientCascade),
            j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.ClientCascade));

        builder.HasMany(u => u.DislikedPosts)
               .WithMany(p => p.UserDislikes)
               .UsingEntity<Dictionary<string, object>>(
            "UsersDislikedPosts",
            j => j.HasOne<Post>().WithMany().OnDelete(DeleteBehavior.ClientCascade),
            j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.ClientCascade)); ;
    }
}
