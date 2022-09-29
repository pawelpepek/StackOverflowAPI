using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;

namespace StackOverflowAPI.Services;

public interface IQuestionService
{
    Task<QuestionDto> AddNewQuestion(string email, string content);
}

public class QuestionService : IQuestionService
{
    private readonly StackOverflowDbContext _db;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public QuestionService(StackOverflowDbContext db, IMapper mapper, IUserService userService)
    {
        this._db = db;
        this._mapper = mapper;
        this._userService = userService;
    }

    public async Task<QuestionDto> AddNewQuestion(string email, string content)
    {
        var user = await _userService.FindUser(email);

        var newQuestion = new Question() { AuthorId = user.Id, Content = content };

        _db.Questions.Add(newQuestion);

        await _db.SaveChangesAsync();

        return _mapper.Map<QuestionDto>(newQuestion);
    }
}
