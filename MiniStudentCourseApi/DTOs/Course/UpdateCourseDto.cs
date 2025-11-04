namespace MiniStudentCourseApi.DTOs.Course
{
    public class UpdateCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int MonthlyPayment { get; set; }

        public List<int> Students { get; set; } = new List<int>();
    }
}
