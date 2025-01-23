using FluentValidation;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Validator
{
    public class FacultyValidation : AbstractValidator<FacultyModel>
    {
        public FacultyValidation()
        {

            RuleFor(faculty => faculty.FacultyName)
                .NotEmpty().WithMessage("Faculty Name is required.")
                .MaximumLength(50).WithMessage("Faculty Name must not exceed 50 characters.");

            RuleFor(faculty => faculty.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email format.");

            RuleFor(faculty => faculty.PhoneNo)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone Number must be 10 digits.");

            RuleFor(faculty => faculty.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(20).WithMessage("Password must not exceed 20 characters.");
        }
    }
}
