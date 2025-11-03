using MiniStudentCourseApi.Model.Enums;

namespace MiniStudentCourseApi.DTOs.Course
{
    public class CreateStudentInCourseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}
