using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowAPI.Entities.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasOne(m => m.Author)
               .WithMany(u => u.Answers)
               .HasForeignKey(m => m.AuthorId)
               .IsRequired();

        builder.HasOne(a => a.Question)
               .WithMany(m => m.Answers)
               .HasForeignKey(a => a.QuestionId)
               .OnDelete(DeleteBehavior.ClientCascade)
               .IsRequired();
    }
}
