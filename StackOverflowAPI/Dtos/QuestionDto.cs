namespace StackOverflowAPI.Dtos
{
    public class QuestionDto:PostDto
    {
        public List<PostDto> Answers { get; set; }
    }
}
