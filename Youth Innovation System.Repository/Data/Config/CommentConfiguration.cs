using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Repository.Data.Config
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>

    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //mapping 1 comment: many reaction on that comment 
            builder.HasMany(c => c.Reactions)
                .WithOne(r => r.comment)
                .HasForeignKey(r => r.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Self-referencing for Replies (one-to-many)
            builder.HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            // Relationship with Post (many-to-one)
            builder.HasOne(c => c.post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.postId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
