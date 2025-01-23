using FluentValidation;
using WebProjectManagement.Model;

namespace WebProjrctManagement.Validator
{
    public class StudentValidation : AbstractValidator<StudentsModel>
    {
        public StudentValidation()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage("Student Name is required.")
                .MaximumLength(100).WithMessage("Student Name should not exceed 100 characters.");

            RuleFor(x => x.Enr_No)
                .NotEmpty().WithMessage("Enrollment Number is required.")
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("Enrollment Number can only contain letters and numbers.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email format.");

            RuleFor(x => x.PhoneNo)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone Number must be 10 digits.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(20).WithMessage("Password must not exceed 20 characters.");
        }

    }
}
