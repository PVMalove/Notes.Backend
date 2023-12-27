using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private readonly INotesDbContext dbContext;

    public UpdateNoteCommandHandler(INotesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        Note? entity = await dbContext.Notes
            .FirstOrDefaultAsync(note => note.ID == request.ID, cancellationToken);
        
        if (entity == null || entity.UserID != request.UserID)
        {
            throw new NotFoundException(nameof(Note), request.ID);
        }
        
        entity.Title = request.Title;
        entity.Details = request.Details;
        entity.EditDate = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}