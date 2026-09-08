using FluentValidation;
using Sindibad.Api.Requests;

namespace Sindibad.Api.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Task title is required.")
            .MaximumLength(150)
            .WithMessage("Task title cannot exceed 150 characters.");
    }
}
