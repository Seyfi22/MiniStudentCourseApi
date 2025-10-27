using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MiniStudentCourseApi.Model.Entities;

namespace MiniStudentCourseApi.Data
{
    public class StudentCourseDbContext : DbContext
    {
        public StudentCourseDbContext(DbContextOptions<StudentCourseDbContext> options) : base(options) { } 

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentCourseDbContext).Assembly);
        }

        
    }
}
