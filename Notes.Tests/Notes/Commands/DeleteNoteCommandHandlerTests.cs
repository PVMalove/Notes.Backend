using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Commands;

public class DeleteNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteNoteCommandHandler_Success() 
    {
        // Arrange
        DeleteNoteCommandHandler handler = new DeleteNoteCommandHandler(Context);
        // Act
        await handler.Handle(
            new DeleteNoteCommand
            {
                ID = NotesContextFactory.NoteIDForDelete,
                UserID = NotesContextFactory.UserAID
            },
            CancellationToken.None);
        // Assert
        Assert.Null(
            await Context.Notes.SingleOrDefaultAsync(note => note.ID == NotesContextFactory.NoteIDForDelete));
    }

    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongId()
    {
        // Arrange
        DeleteNoteCommandHandler handler = new DeleteNoteCommandHandler(Context);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new DeleteNoteCommand
                {
                    ID = Guid.NewGuid(),
                    UserID = NotesContextFactory.UserAID
                },
                CancellationToken.None));
    }

    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
    {
        // Arrange
        DeleteNoteCommandHandler? deleteHandler = new DeleteNoteCommandHandler(Context);
        CreateNoteCommandHandler? createHandler = new CreateNoteCommandHandler(Context);
        Guid noteID = await createHandler.Handle(
            new CreateNoteCommand
            {
                UserID = NotesContextFactory.UserAID,
                Title = "note title",
                Details = "note details"
            },
            CancellationToken.None);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await deleteHandler.Handle(
                new DeleteNoteCommand
                {
                    ID = noteID,
                    UserID = NotesContextFactory.UserBID
                },
                CancellationToken.None));
        
    }
}