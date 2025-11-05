using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniStudentCourseApi.Data;
using MiniStudentCourseApi.DTOs.Student;
using MiniStudentCourseApi.Model.Entities;
using MiniStudentCourseApi.Model.Enums;
using MiniStudentCourseApi.Services.Interfaces;

namespace MiniStudentCourseApi.Services.Implementations
{
    public class StudentService : GenericService<StudentDto, Student>, IStudentService
    {
        public StudentService(StudentCourseDbContext context, IMapper mapper) : base(context, mapper) { }

        public override List<StudentDto> GetAll()
        {
            var students = _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToList();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public override StudentDto GetById(int id)
        {
            var student = _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.Id == id);

            if(student == null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found");
            }

            return _mapper.Map<StudentDto>(student);
        }

        public StudentDto AddWithCourses(CreateStudentDto createStudentDto)
        {
            if (createStudentDto == null)
            {
                throw new ArgumentNullException(nameof(createStudentDto), "Dto can not be null");
            }

            var student = _mapper.Map<Student>(createStudentDto);

            _context.Students.Add(student);
            _context.SaveChanges();

            return _mapper.Map<StudentDto>(student);
        }

        public StudentDto UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            if(updateStudentDto == null)
            {
                throw new ArgumentNullException(nameof(updateStudentDto), "Dto can not be null");
            }

            var student = _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.Id == id);

            if(student == null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found");
            }

            student.FirstName = updateStudentDto.FirstName;
            student.LastName = updateStudentDto.LastName;
            student.BirthDay = updateStudentDto.BirthDay;
            student.Gender = Enum.Parse<Gender>(updateStudentDto.Gender);
            student.Email = updateStudentDto.Email;

            student.Enrollments.Clear();

            if(updateStudentDto.Courses != null && updateStudentDto.Courses.Any())
            {
                var validCourseIds = _context.Courses
                    .Where(c => updateStudentDto.Courses.Contains(c.Id))
                    .Select(c => c.Id)
                    .ToList();

                student.Enrollments = validCourseIds.Select(cid => new Enrollment
                {
                    CourseId = cid,
                    StudentId = student.Id
                }).ToList();
            }

            _context.SaveChanges();

            return _mapper.Map<StudentDto>(student);
            
        }
    }
}  
