using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;

namespace Notes.Tests.Notes.Queries;

[Collection("QueryCollection")]
public class GetNoteDetailsQueryHandlerTests
{
    private readonly NotesDbContext Context;
    private readonly IMapper Mapper;

    public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetNoteDetailsQueryHandler_Success()
    {
        // Arrange
        GetNoteDetailsQueryHandler handler = new GetNoteDetailsQueryHandler(Context, Mapper);
        // Act
        NoteDetailsViewModel result = await handler.Handle(
            new GetNoteDetailsQuery
            {
                ID = Guid.Parse("7B3471A5-B38C-4C4B-B951-7467D5236A9C"),
                UserID = NotesContextFactory.UserBID,
            },
            CancellationToken.None);
        // Assert
        result.ShouldBeOfType<NoteDetailsViewModel>();
        result.Title.ShouldBe("Note 2");
        result.CreationDate.ShouldBe(DateTime.Today);
    }
}