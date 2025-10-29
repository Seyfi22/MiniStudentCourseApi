using MiniStudentCourseApi.Model.DTOs;
using MiniStudentCourseApi.Model.Entities;

namespace MiniStudentCourseApi.ManualMappings
{
    public static class EnrollmentMapper
    {
        public static Enrollment MapEnrollmentDtoToEnrollment(EnrollmentDto dto)
        {
            if(dto == null)
            {
                return null;
            }


            var enrollment = new Enrollment
            {
                Id = dto.Id,
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrollmentDate = dto.EnrollmentDate,
            };

            if(!string.IsNullOrEmpty(dto.Student))
            {
                var nameParts = dto.Student.Split(' ');

                enrollment.Student = new Student
                {
                    FirstName = nameParts[0],
                    LastName = nameParts.Length > 1 ? nameParts[1] : ""
                };
            }

            if (!string.IsNullOrEmpty(dto.Course))
            {
                enrollment.Course = new Course
                {
                    Name = dto.Course
                };
            }

            return enrollment;            
        }
    }
}
