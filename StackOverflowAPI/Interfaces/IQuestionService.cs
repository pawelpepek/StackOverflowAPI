using StackOverflowAPI.Dtos;

namespace StackOverflowAPI.Interfaces;

public interface IQuestionService
{
    Task<QuestionDto> AddNewQuestion(string email, string content);
    List<QuestionDto> GetUserQuestions(string email);
    Task<PostDto> AddAnswer(long questionId, CreateAnswerDto answerDto);
    Task DeleteQuestion(long questionId);
    QuestionDto GetQuestion(long questionId);
}
