using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;

namespace Notes.Tests.Common;

public class NotesContextFactory
{
    public static Guid UserAID = Guid.NewGuid();
    public static Guid UserBID = Guid.NewGuid();
    
    public static Guid NoteIDForDelete = Guid.NewGuid();
    public static Guid NoteIDForUpdate = Guid.NewGuid();

    public static NotesDbContext Create()
    {
        DbContextOptions<NotesDbContext> options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        NotesDbContext context = new NotesDbContext(options);
        context.Database.EnsureCreated();
        context.Notes.AddRange(
            new Note
            {
                CreationData = DateTime.Today,
                Details = "Details for Note 1",
                EditDate = null,
                ID = Guid.Parse("069912C2-6486-47B1-B127-C77B312E4F79"),
                Title = "Note 1",
                UserID = UserAID
                
            },
            new Note
            {
                CreationData = DateTime.Today,
                Details = "Details for Note 2",
                EditDate = null,
                ID = Guid.Parse("7B3471A5-B38C-4C4B-B951-7467D5236A9C"),
                Title = "Note 2",
                UserID = UserBID
            },
            new Note
            {
                CreationData = DateTime.Today,
                Details = "Details for Note 3",
                EditDate = null,
                ID = NoteIDForDelete,
                Title = "Note 3",
                UserID = UserAID
            },
            new Note
            {
                CreationData = DateTime.Today,
                Details = "Details for Note 4",
                EditDate = null,
                ID = NoteIDForUpdate,
                Title = "Note 4",
                UserID = UserBID
            }
        );
        context.SaveChanges();
        return context;
    }

    public static void Destroy(NotesDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}