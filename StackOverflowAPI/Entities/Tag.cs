namespace StackOverflowAPI.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
