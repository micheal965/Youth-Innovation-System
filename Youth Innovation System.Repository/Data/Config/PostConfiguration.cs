using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Repository.Data.Config
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(p => p.Reacts)
                .WithOne(r => r.post)
                .HasForeignKey(r => r.postId)
                .OnDelete(DeleteBehavior.Cascade);//Delete reacts if post is deleted

            builder.HasMany(p => p.Comments)
                .WithOne(r => r.post)
                .HasForeignKey(r => r.postId)
                .OnDelete(DeleteBehavior.Cascade);//Delete comments if post is deleted
        }
    }
}
