namespace StackOverflowAPI.Dtos;

public class MessageDto
{
    public string Content { get; set; }
    public UserDto Author { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Edited { get; set; }
}
