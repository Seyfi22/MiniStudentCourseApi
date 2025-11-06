using MiniStudentCourseApi.DTOs.Student;

namespace MiniStudentCourseApi.Services.Interfaces
{
    public interface IStudentService : IGenericService<StudentDto>
    {
        public StudentDto AddWithCourses(CreateStudentDto createStudentDto);
        public StudentDto UpdateStudent(int id, UpdateStudentDto updateStudentDto);
        public bool IsEmailRegisteredByAnotherAccount(int currentStudentId, string email);
    }
}
