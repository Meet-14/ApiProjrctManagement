using FluentValidation;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Validator
{
    public class ProjectValidation : AbstractValidator<ProjectModel>
    {
        public ProjectValidation()
        {
            RuleFor(p => p.ProjectDefinition)
                .NotEmpty().WithMessage("Project Defination id Required")
                .MaximumLength(500).WithMessage("Project Definition should not exceed 500 characters.");
        }
    }
}
