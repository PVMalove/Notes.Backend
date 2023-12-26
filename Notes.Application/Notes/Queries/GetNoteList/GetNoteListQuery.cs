using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQuery : IRequest<NoteListViewModel>
{
    public Guid UserID { get; set; }
}