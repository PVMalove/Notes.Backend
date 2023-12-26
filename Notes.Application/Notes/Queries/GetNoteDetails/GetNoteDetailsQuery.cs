using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailsQuery : IRequest<NoteDetailsViewModel>
{
    public Guid UserID { get; set; }
    public Guid ID { get; set; }
}