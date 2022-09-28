namespace StackOverflowAPI.Entities;

public abstract class Message
{
    public long Id { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Edited { get; set; }

    public virtual User Author { get; set; }
}
