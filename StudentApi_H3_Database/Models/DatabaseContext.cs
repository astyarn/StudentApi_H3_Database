using Microsoft.EntityFrameworkCore;

namespace StudentApi_H3_Database.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<StudentCourse> StudentCourses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(cl => new
                {
                    cl.StudentId,
                    cl.CourseId
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
