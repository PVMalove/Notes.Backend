using MediatR;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommand : IRequest
{
    public Guid UserID { get; set; }
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
}