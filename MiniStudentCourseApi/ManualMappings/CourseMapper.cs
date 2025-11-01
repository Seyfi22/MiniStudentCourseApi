using MiniStudentCourseApi.DTOs;
using MiniStudentCourseApi.Model.Entities;

namespace MiniStudentCourseApi.ManualMappings
{
    public static class CourseMapper
    {
        public static Course MapCourseDtoToCourse(CourseDto dto)
        {
            if(dto == null)
            {
                return null;
            }

            var course = new Course
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location,
                MonthlyPayment = dto.MonthlyPayment
            };

            if(dto.Students != null && dto.Students.Any())
            {
                course.Enrollments = dto.Students.Select(s => new Enrollment
                {
                    StudentId = s.Id,
                    CourseId = course.Id
                }).ToList();
            }

            return course;
        }
    }
}
