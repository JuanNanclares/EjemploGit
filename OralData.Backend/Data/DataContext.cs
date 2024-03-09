using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OralData.Shared.Entities;

namespace OralData.Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Specialtie> Specialties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseEnrolled> CoursesEnrolled { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<ClassificationSurvey> ClassificationSurveys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Specialtie>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(s => new { s.Name, s.CountryId }).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.Name, c.StateId }).IsUnique();
        }
    }
}