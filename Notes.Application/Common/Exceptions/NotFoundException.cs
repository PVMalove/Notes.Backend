namespace Notes.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string noteName, Guid requestId) : base(
        $"Entity \"{noteName}\" ({requestId}) was not found.") { }
}