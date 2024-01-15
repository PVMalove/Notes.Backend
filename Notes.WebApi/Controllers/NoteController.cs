using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Model;

namespace Notes.WebApi.Controllers;

[ApiVersion("1.0")]
[ApiVersion("2.0")]
//[ApiVersionNeutral]
[Produces("application/json")]
[Route("api/{version:apiVersion}/[controller]")]
public class NoteController : BaseController
{
    private readonly IMapper mapper;

    public NoteController(IMapper mapper)
    {
        this.mapper = mapper;
    }
    
    /// <summary>
    /// Gets the list of notes
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note
    /// </remarks>
    /// <returns>Returns NoteListViewModel</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListViewModel>> GetAll()
    {
        GetNoteListQuery query = new GetNoteListQuery { UserID = UserID };
        NoteListViewModel vm = await Mediator?.Send(query)!;
        return Ok(vm);
    }

    /// <summary>
    /// Gets the note by Id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note/A13040C3-135F-41ED-9C0D-42360B188C87
    /// </remarks>
    /// <param name="id">Note Id (guid)</param>
    /// <returns>Returns NoteDetailsViewModel</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteDetailsViewModel>> Get(Guid id)
    {
        GetNoteDetailsQuery query = new GetNoteDetailsQuery { ID = id, UserID = UserID };
        NoteDetailsViewModel vm = await Mediator?.Send(query)!;
        return Ok(vm);
    }

    /// <summary>
    /// Creates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// Post /note
    /// {
    ///     title: "note title",
    ///     details: "note details
    /// }
    /// </remarks>
    /// <param name="command">CreateNoteDto object</param>
    /// <returns>Returns Id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDTO command)
    {
        var mappedCommand = mapper.Map<CreateNoteCommand>(command);
        mappedCommand.UserID = UserID;
        var noteId = await Mediator?.Send(mappedCommand)!;
        return Created($"{Request.Path}/{noteId}", noteId);
    }

    /// <summary>
    /// Update the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /note
    /// {
    ///     title: "update note title"
    /// }
    /// </remarks>
    /// <param name="command">UpdateNoteDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Update([FromBody] UpdateNoteDTO command)
    {
        UpdateNoteCommand? mappedCommand = mapper.Map<UpdateNoteCommand>(command);
        mappedCommand.UserID = UserID;
        await Mediator?.Send(mappedCommand)!;
        return NoContent();
    }
    
    /// <summary>
    /// Deletes the note by Id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE note/39225E92-08EC-466C-9388-31ED38455445
    /// </remarks>
    /// <param name="id">Id of the note (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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