using MiniStudentCourseApi.DTOs.Student;

namespace MiniStudentCourseApi.Services.Interfaces
{
    public interface IStudentService : IGenericService<StudentDto>
    {
        public StudentDto AddWithCourses(CreateStudentDto createStudentDto);
    }
}
