using AutoMapper;
using MiniStudentCourseApi.Model.DTOs;
using MiniStudentCourseApi.Model.Entities;

namespace MiniStudentCourseApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Enrollments.Select(e => e.Course)));

            // Manual Mapping was performed to solve the following mapping
            //CreateMap<StudentDto, Student>()
            //    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)));


            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Enrollments.Select(e => e.Student)));


            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.Name));

            // The reverse mapping(Dto -> Entity) won't work, so we are gonna use Manual Mapping
        }
    }
}
