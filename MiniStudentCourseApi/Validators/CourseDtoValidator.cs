using FluentValidation;
using MiniStudentCourseApi.DTOs;

namespace MiniStudentCourseApi.Validators
{
    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Course name is required")
                .MaximumLength(50).WithMessage("Course name must be at most 50 characters");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(255).WithMessage("Description must be at most 255 characters");

            RuleFor(c => c.Location)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(255).WithMessage("Location must be at most 255 characters");

            RuleFor(c => c.MonthlyPayment)
                .NotEmpty().WithMessage("Montly payment is required")
                .GreaterThan(100).WithMessage("Monthly payment must be greater than 100");
        }
    }
}
