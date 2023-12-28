using FluentValidation;

namespace Notes.Application.Notes.Commands.CreateNote;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(createNoteCommand => createNoteCommand.UserID).NotEqual(Guid.Empty);
        RuleFor(createNoteCommand => createNoteCommand.Title).NotEmpty().MaximumLength(250);
    }
}