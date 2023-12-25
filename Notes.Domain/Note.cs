namespace Notes.Domain;

public class Note
{
    public Guid UserID { get; set; }
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public DateTime CreationData { get; set; }
    public DateTime? EditDate { get; set; }
}