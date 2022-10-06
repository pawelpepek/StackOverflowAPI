using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;
using StackOverflowAPI.Interfaces;

namespace StackOverflowAPI.Services;

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

    public async Task<QuestionDto> AddNewQuestion(CreateQuestionDto dto)
    {
        var user = await _userService.FindUser(dto.AuthorEmail);

        var newQuestion = new Question()
        {
            AuthorId = user.Id,
            Content = dto.Content,
            Tags = _db.Tags.Where(t => dto.TagsIds.Contains(t.Id)).ToList()
        };

        _db.Questions.Add(newQuestion);

        await _db.SaveChangesAsync();

        return _mapper.Map<QuestionDto>(newQuestion);
    }

    public List<QuestionDto> GetUserQuestions(string email)
    {
        return QuestionsWithAdditionalInfo()
            .Where(q => q.Author.Email.ToLower() == email.ToLower().Trim())
            .Select(_mapper.Map<QuestionDto>)
            .ToList();
    }

    public QuestionDto GetQuestion(long questionId)
    {
        var question = QuestionsWithAdditionalInfo().FirstOrDefault(q => q.Id == questionId);

        if (question == null)
        {
            throw new Exception("Question doesn't exist!");
        }
        else
        {
            return _mapper.Map<QuestionDto>(question);
        }
    }

    private IEnumerable<Question> QuestionsWithAdditionalInfo()
    {
        return _db.Questions
            .AsNoTracking()
            .Include(q => q.Votes)
            .Include(q => q.Tags)
            .Include(q => q.Author)
            .Include(q => q.Answers)
                .ThenInclude(qu => qu.Author)
            .Include(q => q.Answers)
                .ThenInclude(qa => qa.Comments)
                    .ThenInclude(ac => ac.Author)
            .Include(q => q.Answers)
                .ThenInclude(a => a.Votes)
            .Include(q => q.Comments)
                .ThenInclude(c => c.Author);
    }
}
