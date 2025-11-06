using FluentValidation;
using MiniStudentCourseApi.Data;
using MiniStudentCourseApi.DTOs.Student;
using MiniStudentCourseApi.Model.Enums;
using MiniStudentCourseApi.Services.Implementations;
using MiniStudentCourseApi.Services.Interfaces;

namespace MiniStudentCourseApi.Validators.Student
{
    public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {

            RuleFor(s => s.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must be at most 50 characters");

            RuleFor(s => s.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must be at most 50 characters");

            RuleFor(s => s.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => Enum.TryParse<Gender>(g, true, out _))
                    .WithMessage($"Gender must be one of the following: {string.Join(", ", Enum.GetNames(typeof(Gender)))}");

            RuleFor(s => s.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(100).WithMessage("Email must be at most 100 characters")
                .When(s => !string.IsNullOrEmpty(s.Email));

            RuleFor(s => s.BirthDay)
                .NotEmpty().WithMessage("Birthday is required")
                .Must(date => date <= DateTime.Now.AddYears(-18))
                    .WithMessage("Student must be at least 18 years old")
                .Must(date => date >= DateTime.Now.AddYears(-35))
                    .WithMessage("Student must be at most 35 years old");
        }
    }
}
