using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(updateNoteCommand => updateNoteCommand.ID).NotEqual(Guid.Empty);
        RuleFor(updateNoteCommand => updateNoteCommand.UserID).NotEqual(Guid.Empty);
        RuleFor(updateNoteCommand => updateNoteCommand.Title).NotEmpty().MaximumLength(255);
    }
}