using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
    public GetNoteDetailsQueryValidator()
    {
        RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.ID).NotEqual(Guid.Empty);
        RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.UserID).NotEqual(Guid.Empty);
    }
}