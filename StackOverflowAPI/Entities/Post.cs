namespace StackOverflowAPI.Entities;

public abstract class Post : Message
{
    public virtual List<User> UserDislikes { get; set; } = new();
    public virtual List<User> UserLikes { get; set; } = new();
    public virtual List<Comment> Comments { get; set; }


    public int Rank => UserLikes.Count - UserDislikes.Count;
}
