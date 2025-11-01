using MiniStudentCourseApi.DTOs;

namespace MiniStudentCourseApi.Services.Interfaces
{
    public interface ICourseService
    {
        public List<CourseDto> GetCoursesWithStudents();
        public CourseDto GetCourseWithStudentsById(int id);
    }
}
