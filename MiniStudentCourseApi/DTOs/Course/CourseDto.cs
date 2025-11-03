namespace MiniStudentCourseApi.DTOs.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int MonthlyPayment { get; set; }

        public ICollection<StudentInCourseDto> Students { get; set; } = new List<StudentInCourseDto>();
    }
}
