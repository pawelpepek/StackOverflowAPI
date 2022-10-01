using StackOverflowAPI.Dtos;

namespace StackOverflowAPI.Interfaces;

public interface IQuestionService
{
    Task<QuestionDto> AddNewQuestion(string email, string content);
    List<QuestionDto> GetUserQuestions(string email);
    QuestionDto GetQuestion(long questionId);
}
