using FluentValidation;
using Sindibad.Api.Requests;

namespace Sindibad.Api.Validators;

public class CreateProjectRequestValidator
    : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Project name is required.")
            .MaximumLength(50)
            .WithMessage("Project name cannot exceed 50 characters.");
    }
}