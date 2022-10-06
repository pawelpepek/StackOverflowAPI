namespace StackOverflowAPI.Entities;

public abstract class Post : Message
{
    public virtual List<Vote> Votes { get; set; } = new();
    public virtual List<Comment> Comments { get; set; }

    public int Rank => Votes.Sum(v => v.Like ? 1 : -1);
}
