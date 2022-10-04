namespace StackOverflowAPI.Dtos
{
    public class CreateQuestionDto: CreateMessageDto
    {
        public List<int> TagsIds { get; set; }
    }
}
