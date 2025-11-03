namespace MiniStudentCourseApi.DTOs.Student
{
    public class CreateCourseInStudentDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int MonthlyPayment { get; set; }
    }
}
