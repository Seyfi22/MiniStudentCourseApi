namespace MiniStudentCourseApi.Model.Entities
{
    public class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int MonthlyPayment { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
