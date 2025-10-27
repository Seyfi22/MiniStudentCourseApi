using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniStudentCourseApi.Model.Entities;

namespace MiniStudentCourseApi.Model.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> entity)
        {
            entity.Property(e=> e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.MonthlyPayment)
                .IsRequired();
        }
    }
}
