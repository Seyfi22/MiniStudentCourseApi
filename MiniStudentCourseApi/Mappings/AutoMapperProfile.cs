using AutoMapper;
using MiniStudentCourseApi.DTOs;
using MiniStudentCourseApi.DTOs.Course;
using MiniStudentCourseApi.DTOs.Student;
using MiniStudentCourseApi.Model.Entities;
using MiniStudentCourseApi.Model.Enums;

namespace MiniStudentCourseApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // While getting the student(s)
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Enrollments.Select(e => new CourseInStudentDto
                {
                    Id = e.Course.Id,
                    Name = e.Course.Name
                })));

            // While posting a new student
            CreateMap<CreateStudentDto, Student>()
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Courses.Select(c => new Enrollment
                {
                    Course = new Course
                    {
                        Name = c.Name,
                        Description = c.Description,
                        Location = c.Location,
                        MonthlyPayment = c.MonthlyPayment,
                    }
                })));

            // While updating the student
            CreateMap<UpdateStudentDto, Student>()
                .ForMember(dest => dest.Enrollments, opt => opt.Ignore());


            // While getting the course(s)
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Enrollments.Select(e => new StudentInCourseDto
                {
                    Id = e.Student.Id,
                    Name = e.Student.FirstName + " " + e.Student.LastName
                })));


            // While posting a new course
            CreateMap<CreateCourseDto, Course>()
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Students.Select(s => new Enrollment
                {
                    Student = new Student
                    {
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Email = s.Email,
                        Gender = Enum.Parse<Gender>(s.Gender),
                        BirthDay = s.Birthday,
                    }
                })));

            // While updating the course
            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.Enrollments, opt => opt.Ignore());
           }
    }
}
