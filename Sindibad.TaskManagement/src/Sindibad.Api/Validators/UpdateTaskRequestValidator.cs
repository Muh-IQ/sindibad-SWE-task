using FluentValidation;
using Sindibad.Api.Requests;

namespace Sindibad.Api.Validators;

public class UpdateTaskRequestValidator
    : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Task title is required.")
            .MaximumLength(150)
            .WithMessage("Task title cannot exceed 150 characters.");
    }
}