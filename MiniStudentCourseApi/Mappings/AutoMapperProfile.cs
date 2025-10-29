using AutoMapper;
using MiniStudentCourseApi.Model.DTOs;
using MiniStudentCourseApi.Model.Entities;
using MiniStudentCourseApi.Model.Enums;

namespace MiniStudentCourseApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

            CreateMap<StudentDto, Student>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)));


            CreateMap<Course, CourseDto>().ReverseMap();


            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.Name));

            // The reverse mapping(Dto -> Entity) won't work, so we are gonna use Manual Mapping
        }
    }
}
