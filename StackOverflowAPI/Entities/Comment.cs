namespace StackOverflowAPI.Entities;

public class Comment : Message
{
    public long PostId { get; set; }
    public virtual Post Post { get; set; }
}
