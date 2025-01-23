using FluentValidation;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Validator
{
    public class StudentProjectValidation : AbstractValidator<StudentProjectModel>
    {
        public StudentProjectValidation()
        {
            RuleFor(x => x.ProjectID)
                    .NotEmpty().WithMessage("Project ID is required.");

            RuleFor(x => x.StudentID)
                .NotEmpty().WithMessage("Student ID is required.");

            RuleFor(x => x.FacultyID)
                .NotEmpty().WithMessage("Faculty ID is required.");

            RuleFor(x => x.AcademicYear)
                .NotEmpty().WithMessage("Academic Year is required.")
                .Matches(@"^\d{4}/\d{4}$").WithMessage("Academic Year must be in the format YYYY/YYYY.");
        }
    }
}
