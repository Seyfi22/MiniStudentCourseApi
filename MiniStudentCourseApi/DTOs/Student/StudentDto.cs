using MiniStudentCourseApi.DTOs.Course;

namespace MiniStudentCourseApi.DTOs.Student
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }

        public ICollection<CourseInStudentDto> Courses { get; set; } = new List<CourseInStudentDto>();
    }
}
