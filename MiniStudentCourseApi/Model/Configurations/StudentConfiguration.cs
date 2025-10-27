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
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Gender)
                .HasConversion<string>()
                .IsRequired();

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
