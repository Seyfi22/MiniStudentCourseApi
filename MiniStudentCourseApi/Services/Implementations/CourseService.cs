using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniStudentCourseApi.Data;
using MiniStudentCourseApi.ManualMappings;
using MiniStudentCourseApi.Model.DTOs;
using MiniStudentCourseApi.Model.Entities;
using MiniStudentCourseApi.Services.Interfaces;

namespace MiniStudentCourseApi.Services.Implementations
{
    public class CourseService : GenericService<CourseDto, Course>
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

        #region Previous Add
        //public override CourseDto Add(CourseDto courseDto)
        //{
        //    if(courseDto == null)
        //    {
        //        throw new ArgumentNullException(nameof(courseDto), "Dto can not be null");
        //    }

        //    var course = _mapper.Map<Course>(courseDto);

        //    if(courseDto.Students != null && courseDto.Students.Any())
        //    {
        //        var existingStudentIds = _context.Students
        //            .Where(s => courseDto.Students.Select(cs => cs.Id).Contains(s.Id))
        //            .Select(s => s.Id)
        //            .ToList();

        //        course.Enrollments = existingStudentIds.Select(id => new Enrollment
        //        {
        //            StudentId = id,
        //            Course = course
        //        }).ToList();
        //    }

        //    _context.Courses.Add(course);
        //    _context.SaveChanges();

        //    return _mapper.Map<CourseDto>(course);
        //}
        #endregion

        public override CourseDto Add(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto), "Dto can not be null");
            }

            var course = CourseMapper.MapCourseDtoToCourse(courseDto);

            if (courseDto.Students != null && courseDto.Students.Any())
            {
                var existingStudentIds = _context.Students
                    .Where(s => courseDto.Students.Select(cs => cs.Id).Contains(s.Id))
                    .Select(s => s.Id)
                    .ToList();

                course.Enrollments = existingStudentIds.Select(id => new Enrollment
                {
                    StudentId = id,
                    Course = course
                }).ToList();
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return _mapper.Map<CourseDto>(course);
        }

        public override CourseDto Update(int id, CourseDto courseDto)
        {
            if(courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto), "Dto can not be null");
            }

            var course = _context.Courses
                .Include(c => c.Enrollments)
                .FirstOrDefault(c => c.Id == id);

            if(course == null)
            {
                throw new KeyNotFoundException($"Course with id {id} not found");
            }

            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.Location = courseDto.Location;
            course.MonthlyPayment = courseDto.MonthlyPayment;

            if(courseDto.Students != null)
            {
                course.Enrollments.Clear();

                var existingStudentIds = _context.Students
                    .Where(s => courseDto.Students.Select(cs => cs.Id).Contains(s.Id))
                    .Select(s => s.Id)
                    .ToList();

                course.Enrollments = existingStudentIds.Select(id => new Enrollment
                {
                    StudentId = id,
                    CourseId = course.Id
                }).ToList();
            }

            _context.SaveChanges();

            return _mapper.Map<CourseDto>(course);
        }

        public override bool Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
