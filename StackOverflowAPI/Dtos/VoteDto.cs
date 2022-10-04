namespace StackOverflowAPI.Dtos;

public class VoteDto
{
    public bool Like { get; set; }
    public string UserEmail { get; set; }
    public long PostId { get; set; }
}
