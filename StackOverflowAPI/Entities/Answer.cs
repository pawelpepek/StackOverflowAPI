namespace StackOverflowAPI.Entities;

public class Answer:Post
{
    public long QuestionId { get; set; }

    public virtual Question Question { get; set; }
}
