using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Model;

namespace Notes.WebApi.Controllers;

[Route("api/[controller]")]
public class NoteController : BaseController
{
    private readonly IMapper mapper;

    public NoteController(IMapper mapper)
    {
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<NoteListViewModel>> GetAll()
    {
        GetNoteListQuery query = new GetNoteListQuery { UserID = UserID };
        NoteListViewModel vm = await Mediator?.Send(query)!;
        return Ok(vm);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NoteDetailsViewModel>> Get(Guid id)
    {
        GetNoteDetailsQuery query = new GetNoteDetailsQuery { ID = id, UserID = UserID };
        NoteDetailsViewModel vm = await Mediator?.Send(query)!;
        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDTO command)
    {
        var mappedCommand = mapper.Map<CreateNoteCommand>(command);
        mappedCommand.UserID = UserID;
        object? noteId = await Mediator?.Send(command)!;
        return Ok(noteId);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateNoteDTO command)
    {
        UpdateNoteCommand? mappedCommand = mapper.Map<UpdateNoteCommand>(command);
        mappedCommand.UserID = UserID;
        await Mediator?.Send(mappedCommand)!;
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        DeleteNoteCommand command = new DeleteNoteCommand
        {
            ID = id,
            UserID = UserID
        };
        await Mediator?.Send(command)!;
        return NoContent();
    }
}