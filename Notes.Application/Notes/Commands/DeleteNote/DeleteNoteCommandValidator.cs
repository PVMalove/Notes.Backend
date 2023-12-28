using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(deleteNoteCommand => deleteNoteCommand.ID).NotEqual(Guid.Empty);
        RuleFor(deleteNoteCommand => deleteNoteCommand.UserID).NotEqual(Guid.Empty);
    }
}