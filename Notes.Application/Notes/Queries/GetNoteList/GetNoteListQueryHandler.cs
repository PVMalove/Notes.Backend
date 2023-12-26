using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListViewModel>
{
    private readonly INotesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetNoteListQueryHandler(INotesDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<NoteListViewModel> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
    {
        List<NoteLookupDTO> notesQuery =  await _dbContext.Notes
            .Where(note => note.UserID == request.UserID)
            .ProjectTo<NoteLookupDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return new NoteListViewModel { Notes = notesQuery };
    }
}