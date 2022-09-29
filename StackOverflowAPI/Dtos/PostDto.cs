namespace StackOverflowAPI.Dtos;

public class PostDto:MessageDto
{
    public List<MessageDto> Comments { get; set; }
    public int Rank { get; set; }
}
