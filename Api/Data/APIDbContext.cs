using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options):base(options){
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorites>()
                .HasKey(f => new { f.UserId, f.QuestionId });

            modelBuilder.Entity<QuestionTags>()
                .HasKey(f => new {f.QuestionId, f.TagId});

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Institution> Institution { get; set; }
        public DbSet<Subject> Subject { set; get; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<QuestionTags> QuestionTags { get; set;}
    }
}
