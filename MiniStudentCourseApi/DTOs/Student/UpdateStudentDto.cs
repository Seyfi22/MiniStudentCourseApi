namespace MiniStudentCourseApi.DTOs.Student
{
    public class UpdateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }

        public List<int> Courses { get; set; } = new List<int>();
    }
}
