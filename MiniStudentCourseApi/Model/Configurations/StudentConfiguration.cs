using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniStudentCourseApi.Model.Entities;

namespace MiniStudentCourseApi.Model.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .HasMaxLength(50);

            entity.Property(e => e.Gender)
                .HasConversion<string>();

            entity.Property(e => e.Email)
                .HasMaxLength(100);
        }
    }
}
