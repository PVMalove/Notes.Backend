using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly INotesDbContext dbContext;

    public CreateNoteCommandHandler(INotesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note
        {
            UserID = request.UserID,
            Title = request.Title,
            Details = request.Details,
            ID = Guid.NewGuid(),
            CreationData = DateTime.Now,
            EditDate = null
        };
        
        await dbContext.Notes.AddAsync(note, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return note.ID;
    }
}