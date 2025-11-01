using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniStudentCourseApi.Data;
using MiniStudentCourseApi.DTOs;
using MiniStudentCourseApi.ManualMappings;
using MiniStudentCourseApi.Model.Entities;
using MiniStudentCourseApi.Model.Enums;

namespace MiniStudentCourseApi.Services.Implementations
{
    public class StudentService : GenericService<StudentDto, Student>
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

        #region Previous Add
        //public override StudentDto Add(StudentDto studentDto)
        //{
        //    if(studentDto == null)
        //    {
        //        throw new ArgumentNullException(nameof(studentDto), "Dto can not be null");
        //    }

        //    var student = _mapper.Map<Student>(studentDto);

        //    if(studentDto.Courses != null && studentDto.Courses.Any())
        //    {
        //        var existingCourseIds = _context.Courses
        //            .Where(c => studentDto.Courses.Select(sc => sc.Id).Contains(c.Id))
        //            .Select(c => c.Id)
        //            .ToList();

        //        student.Enrollments = existingCourseIds.Select(id => new Enrollment
        //        {
        //            CourseId = id,
        //            Student = student
        //        }).ToList();
        //    }

        //    _context.Students.Add(student);
        //    _context.SaveChanges();

        //    return _mapper.Map<StudentDto>(student);
        //}
        #endregion

        public override StudentDto Add(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                throw new ArgumentNullException(nameof(studentDto), "Dto can not be null");
            }

            var student = StudentMapper.MapStudentDtoToStudent(studentDto);

            if (studentDto.Courses != null && studentDto.Courses.Any())
            {
                var existingCourseIds = _context.Courses
                    .Where(c => studentDto.Courses.Select(sc => sc.Id).Contains(c.Id))
                    .Select(c => c.Id)
                    .ToList();

                student.Enrollments = existingCourseIds.Select(id => new Enrollment
                {
                    CourseId = id,
                    Student = student
                }).ToList();
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return _mapper.Map<StudentDto>(student);
        }

        public override StudentDto Update(int id, StudentDto studentDto)
        {
            if(studentDto == null)
            {
                throw new ArgumentNullException(nameof(studentDto), "Dto can not be null");
            }

            var student = _context.Students
                .Include(s => s.Enrollments)
                .FirstOrDefault(s => s.Id == id);

            if(student == null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found");
            }

            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.BirthDay = studentDto.BirthDay;
            student.Gender = Enum.Parse<Gender>(studentDto.Gender);
            student.Email = studentDto.Email;

            if (studentDto.Courses != null)
            {
                student.Enrollments.Clear();

                var existingCourseIds = _context.Courses
                    .Where(c => studentDto.Courses.Select(sc => sc.Id).Contains(c.Id))
                    .Select(c => c.Id)
                    .ToList();

                student.Enrollments = existingCourseIds.Select(id => new Enrollment
                {
                    CourseId = id,
                    StudentId = student.Id
                }).ToList();
            }

            _context.SaveChanges();


            return _mapper.Map<StudentDto>(student);
        }

        public override bool Delete(int id)
        {
            return base.Delete(id);
        }
    }
}  
