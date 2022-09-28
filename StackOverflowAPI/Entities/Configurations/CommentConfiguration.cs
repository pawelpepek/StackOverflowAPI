using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowAPI.Entities.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(m => m.Author)
               .WithMany(u => u.Comments)
               .HasForeignKey(m => m.AuthorId)
               .IsRequired();

        builder.HasOne(m => m.Post)
               .WithMany(p => p.Comments)
               .HasForeignKey(m => m.PostId);
    }
}
