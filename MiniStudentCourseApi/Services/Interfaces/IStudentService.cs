using MiniStudentCourseApi.DTOs;

namespace MiniStudentCourseApi.Services.Interfaces
{
    public interface IStudentService
    {
        public List<StudentDto> GetStudentsWithCourses();
        public StudentDto GetStudentWithCoursesById(int id);
    }
}
