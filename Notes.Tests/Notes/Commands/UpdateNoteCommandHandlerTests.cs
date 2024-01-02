using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Commands;

public class UpdateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateNoteCommandHandler_Success()
    {
        // Arrange
        UpdateNoteCommandHandler handler = new UpdateNoteCommandHandler(Context);
        string updateTitle = "new title";
        // Act
        await handler.Handle(
            new UpdateNoteCommand
            {
                ID = NotesContextFactory.NoteIDForUpdate,
                UserID = NotesContextFactory.UserBID,
                Title = updateTitle,
            },
            CancellationToken.None);
        // Assert
        Assert.NotNull(
            await Context.Notes.SingleOrDefaultAsync(note =>
                note.ID == NotesContextFactory.NoteIDForUpdate && note.Title == updateTitle));
    }

    [Fact]
    public async Task UpdateNoteCommandHandler_FailOnWrongId()
    {
        // Arrange
        UpdateNoteCommandHandler handler = new UpdateNoteCommandHandler(Context);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new UpdateNoteCommand
                {
                    ID = Guid.NewGuid(),
                    UserID = NotesContextFactory.UserAID,
                },
                CancellationToken.None));
    }

    [Fact]
    public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
    {
        // Arrange
        UpdateNoteCommandHandler updateHandler = new UpdateNoteCommandHandler(Context);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await updateHandler.Handle(new UpdateNoteCommand
                {
                    ID = NotesContextFactory.NoteIDForUpdate,
                    UserID = NotesContextFactory.UserAID,
                },
                CancellationToken.None));
    }
}