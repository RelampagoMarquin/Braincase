using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data{
    public class APIDbContext : IdentityDbContext<User>
    {
        public APIDbContext(DbContextOptions<APIDbContext> options):base(options){
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Favorites>()
                .HasKey(f => new { f.UserId, f.QuestionId });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(t => new { t.Name, t.SubjectId })
                .IsUnique();

            modelBuilder.Entity<Subject>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Institution>()
                .HasIndex(i => i.Name)
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
    }
}
