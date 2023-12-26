using MediatR;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommand : IRequest
{
    public Guid UserID { get; set; }
    public Guid ID { get; set; }
}