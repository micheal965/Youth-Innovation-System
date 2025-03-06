using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Youth_Innovation_System.Core.Entities;

namespace Youth_Innovation_System.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<React> Reacts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
