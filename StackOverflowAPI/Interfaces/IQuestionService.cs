using StackOverflowAPI.Dtos;

namespace StackOverflowAPI.Interfaces;

public interface IQuestionService
{
    Task<QuestionDto> AddNewQuestion(CreateQuestionDto dto);
    List<QuestionDto> GetUserQuestions(string email);
    QuestionDto GetQuestion(long questionId);
}
