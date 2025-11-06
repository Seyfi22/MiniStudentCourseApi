using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniStudentCourseApi.Data;
using MiniStudentCourseApi.DTOs.Course;
using MiniStudentCourseApi.Model.Entities;
using MiniStudentCourseApi.Services.Interfaces;

namespace MiniStudentCourseApi.Services.Implementations
{
    public class CourseService : GenericService<CourseDto, Course>, ICourseService
    {
        public CourseService(StudentCourseDbContext context, IMapper mapper) : base(context, mapper) { }

        public override List<CourseDto> GetAll()
        {
            var courses = _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .ToList();

            return _mapper.Map<List<CourseDto>>(courses);
        }

        public override CourseDto GetById(int id)
        {
            var course = _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefault(c => c.Id == id);

            if(course == null)
            {
                throw new KeyNotFoundException($"Course with id {id} not found");
            }

            return _mapper.Map<CourseDto>(course);
        }


        public CourseDto AddWithStudents(CreateCourseDto createCourseDto)
        {
            if (createCourseDto == null)
            {
                throw new ArgumentNullException(nameof(createCourseDto), "Dto can not be null");
            }

            var course = _mapper.Map<Course>(createCourseDto);

            _context.Courses.Add(course);
            _context.SaveChanges();

            return _mapper.Map<CourseDto>(course);
        }

        public CourseDto UpdateCourse(int id, UpdateCourseDto updateCourseDto)
        {
            if(updateCourseDto == null)
            {
                throw new ArgumentNullException(nameof(updateCourseDto), "Dto can not be null");
            }

            var course = _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefault(c => c.Id == id);

            if(course == null)
            {
                throw new KeyNotFoundException($"Course with id {id} not found");
            }

            course.Name = updateCourseDto.Name;
            course.Description = updateCourseDto.Description;
            course.Location = updateCourseDto.Location;
            course.MonthlyPayment = updateCourseDto.MonthlyPayment;

            course.Enrollments.Clear();

            if(updateCourseDto.Students != null && updateCourseDto.Students.Any())
            {
                var validStudentIds = _context.Students
                    .Where(s => updateCourseDto.Students.Contains(s.Id))
                    .Select(s => s.Id)
                    .ToList();

                course.Enrollments = validStudentIds.Select(sid => new Enrollment
                {
                    StudentId = sid,
                    CourseId = course.Id
                }).ToList();
            }

            _context.SaveChanges();

            return _mapper.Map<CourseDto>(course);
        }

        public bool IsCourseNameRegisteredByAnotherAccount(int currentCourseId, string courseName)
        {
            return _context.Courses.Any(c => c.Name.ToLower() == courseName.ToLower() && c.Id != currentCourseId);
        }
    }
}
