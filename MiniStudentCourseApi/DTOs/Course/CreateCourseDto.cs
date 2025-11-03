namespace MiniStudentCourseApi.DTOs.Course
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int MonthlyPayment { get; set; }

        public ICollection<CreateStudentInCourseDto> Students { get; set; } = new List<CreateStudentInCourseDto>();
    }
}
