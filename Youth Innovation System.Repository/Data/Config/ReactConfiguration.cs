using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Repository.Data.Config
{
    public class ReactConfiguration : IEntityTypeConfiguration<React>
    {
        public void Configure(EntityTypeBuilder<React> builder)
        {
            builder.Property(r => r.reactionType)
                .HasMaxLength(50);

        }
    }
}
