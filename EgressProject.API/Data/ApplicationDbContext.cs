using EgressProject.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EgressProject.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Especialization> Especializations { get; set; }
        public DbSet<Highlights> Highlights { get; set; }
        public DbSet<JobAdvertisement> JobAdvertisements { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonCourse> PersonCourses { get; set; }
        public DbSet<Testimony> Testimonies { get; set; }
        public DbSet<User> Users { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}