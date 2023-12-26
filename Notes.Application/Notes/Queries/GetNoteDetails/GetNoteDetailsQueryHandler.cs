using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsViewModel>
{
    private readonly INotesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetNoteDetailsQueryHandler(INotesDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<NoteDetailsViewModel> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
    {
        Note? entity = await _dbContext.Notes
            .FirstOrDefaultAsync(note => note.ID == request.ID, cancellationToken);

        if (entity == null || entity.UserID != request.UserID)
        {
            throw new NotFoundException(nameof(Note), request.ID);
        }
        
        return _mapper.Map<NoteDetailsViewModel>(entity);
    }
}