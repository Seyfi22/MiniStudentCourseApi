namespace MiniStudentCourseApi.DTOs.Student
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDay { get; set; }

        public ICollection<CreateCourseInStudentDto> Courses { get; set; } = new List<CreateCourseInStudentDto>();
    }
}
