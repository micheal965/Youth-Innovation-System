using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Repository.Data.Config
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>

    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //mapping 1 comment : many reactions on that comment
            builder.HasMany(c => c.CommentReactions)
                .WithOne(cr => cr.comment)
                .HasForeignKey(cr => cr.commentId)
                .OnDelete(DeleteBehavior.Cascade);

            //mapping 1 comment: many replies on that comment 
            builder.HasMany(c => c.CommentReplies)
                .WithOne(cr => cr.comment)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
