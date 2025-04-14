using Microsoft.EntityFrameworkCore;
using Youth_Innovation_System.Core.Entities.Chat;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.Entities.PostAggregate;

namespace Youth_Innovation_System.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<React> Reacts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Message> Messages { get; set; }

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
