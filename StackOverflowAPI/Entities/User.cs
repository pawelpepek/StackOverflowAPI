namespace StackOverflowAPI.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public virtual List<Question> Questions { get; set; }
    public virtual List<Answer> Answers{ get; set; }
    public virtual List<Comment> Comments { get; set; }

    public virtual List<Post> LikedPosts { get; set; }
    public virtual List<Post> DislikedPosts { get; set; }
}
