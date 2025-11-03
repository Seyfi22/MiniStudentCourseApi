using MiniStudentCourseApi.DTOs.Student;
using MiniStudentCourseApi.Model.Entities;
using MiniStudentCourseApi.Model.Enums;

namespace MiniStudentCourseApi.ManualMappings
{
    public static class StudentMapper
    {
        public static Student MapStudentDtoToStudent(StudentDto dto)
        {
            if(dto == null)
            {
                return null;
            }

            var student = new Student
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDay = dto.BirthDay,
                Gender = Enum.Parse<Gender>(dto.Gender),
                Email = dto.Email
            };

            if(dto.Courses != null && dto.Courses.Any())
            {
                student.Enrollments = dto.Courses.Select(c => new Enrollment
                {
                    CourseId = c.Id,
                    StudentId = student.Id
                }).ToList();
            }

            return student;
        }
    }
}
