using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Repository.Data.Config
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(p => p.Reacts)
                .WithOne(r => r.post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Restrict);//Delete reacts if post is deleted

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.post)
                .HasForeignKey(c => c.postId)
                .OnDelete(DeleteBehavior.Cascade);//Delete comments if post is deleted

            builder.HasMany(p => p.postImages)
                .WithOne(pi => pi.post)
                .HasForeignKey(pi => pi.PostId)
                .OnDelete(DeleteBehavior.Cascade);//Delete postimages if post deleted

        }
    }
}
