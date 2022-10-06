namespace StackOverflowAPI.Entities;

public class Vote
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public long PostId { get; set; }
    public bool Like { get; set; }

    public virtual User User { get; set; }
    public virtual Post Post { get; set; }
}
