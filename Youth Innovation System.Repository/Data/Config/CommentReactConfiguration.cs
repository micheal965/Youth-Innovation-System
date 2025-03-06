using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Repository.Data.Config
{
    internal class CommentReactConfiguration : IEntityTypeConfiguration<CommentReaction>

    {
        public void Configure(EntityTypeBuilder<CommentReaction> builder)
        {
            {
                builder.Property(cr => cr.reactionType)
                        .HasMaxLength(50);
            }
        }
    }
}