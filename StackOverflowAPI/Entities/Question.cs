namespace StackOverflowAPI.Entities;

public class Question:Post
{
    public virtual List<Answer> Answers { get; set; }
}
