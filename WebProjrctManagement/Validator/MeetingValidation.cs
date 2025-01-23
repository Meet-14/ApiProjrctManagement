using FluentValidation;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Validator
{
    public class MeetingValidation : AbstractValidator<MeetingModel>
    {
        public MeetingValidation()
        {
            RuleFor(meeting => meeting.StudentID)
                .GreaterThan(0).WithMessage("Student ID must be greater than 0.");

            RuleFor(meeting => meeting.FacultyID)
                .GreaterThan(0).WithMessage("Faculty ID must be greater than 0.");

            RuleFor(meeting => meeting.ProjectID)
                .GreaterThan(0).WithMessage("Project ID must be greater than 0.");

            RuleFor(meeting => meeting.Discussion)
                .NotEmpty().WithMessage("Discussion is required.");

            RuleFor(meeting => meeting.Remark)
                .NotEmpty().When(meeting => !string.IsNullOrEmpty(meeting.Remark))
                .WithMessage("Remark must not be empty.");
        }
    }
}
