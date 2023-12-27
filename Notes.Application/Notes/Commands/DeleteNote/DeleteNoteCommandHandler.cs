using MediatR;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INotesDbContext dbContext;

    public DeleteNoteCommandHandler(INotesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        Note? entity = await dbContext.Notes.FindAsync(new object[] { request.ID }, cancellationToken);

        if (entity == null || entity.UserID != request.UserID)
        {
            throw new NotFoundException(nameof(Note), request.ID);
        }
        
        dbContext.Notes.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}