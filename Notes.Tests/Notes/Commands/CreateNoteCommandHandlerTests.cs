using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Commands;

public class CreateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateNoteCommandHandler_Success()
    {
        // Arrange
        CreateNoteCommandHandler handler = new CreateNoteCommandHandler(Context);
        string noteName = "note name";
        string noteDetails = "note details";
        // Act
        Guid noteID = await handler.Handle(
            new CreateNoteCommand
            {
                Title = noteName,
                Details = noteDetails,
                UserID = NotesContextFactory.UserAID
            },
            CancellationToken.None);
        // Assert
        Assert.NotNull(
            await Context.Notes.SingleOrDefaultAsync(note => 
                note.ID == noteID && note.Title == noteName && note.Details == noteDetails));
    }
}