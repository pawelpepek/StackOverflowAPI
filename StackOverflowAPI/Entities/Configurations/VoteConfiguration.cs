using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowAPI.Entities.Configurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(p => p.User)
                   .WithMany(u => u.Votes)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.ClientCascade)
                   .IsRequired();


            builder.HasOne(p=>p.Post)
                   .WithMany(m=>m.Votes)
                   .HasForeignKey(p=>p.PostId)
                   .OnDelete(DeleteBehavior.ClientCascade)
                   .IsRequired();
        }
    }
}
