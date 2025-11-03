using MiniStudentCourseApi.DTOs.Course;

namespace MiniStudentCourseApi.Services.Interfaces
{
    public interface ICourseService : IGenericService<CourseDto>
    {
        public CourseDto AddWithStudents(CreateCourseDto createCourseDto);
    }
}
