namespace MiniStudentCourseApi.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public string Student { get; set; } // StudentName
        public string Course { get; set; } // CourseName
    }
}
