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
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasMaxLength(255);

            entity.Property(e => e.Location)
                .HasMaxLength(255);

            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.Description).IsUnique();
            entity.HasIndex(e => e.Location).IsUnique();

        }
    }
}
