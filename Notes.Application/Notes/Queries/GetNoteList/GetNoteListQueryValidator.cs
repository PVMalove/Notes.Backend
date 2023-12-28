using FluentValidation;
using Notes.Application.Notes.Queries.GetNoteDetails;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
    public GetNoteListQueryValidator()
    {
        RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.ID).NotEqual(Guid.Empty);
        RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.UserID).NotEqual(Guid.Empty);
    }
}