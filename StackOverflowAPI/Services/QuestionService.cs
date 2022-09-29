﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;

namespace StackOverflowAPI.Services;

public interface IQuestionService
{
    Task<QuestionDto> AddNewQuestion(string email, string content);
    List<QuestionDto> GetUserQuestions(string email);
    Task<PostDto> AddAnswer(long questionId, CreateAnswerDto answerDto);
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

    public List<QuestionDto> GetUserQuestions(string email)
    {
        return _db.Questions
            .AsNoTracking()
            .Include(q => q.Author)
            .Include(q => q.Answers)
            .ThenInclude(qu => qu.Author)
            .Include(q => q.Answers)
            .ThenInclude(qa => qa.Comments)
            .ThenInclude(ac => ac.Author)
            .Include(q => q.Comments)
            .ThenInclude(c => c.Author)
            .Where(q => q.Author.Email.ToLower() == email.ToLower().Trim())
            .Select(_mapper.Map<QuestionDto>)
            .ToList();
    }

    public async Task<PostDto> AddAnswer(long questionId, CreateAnswerDto answerDto)
    {
        var user = await _userService.FindUser(answerDto.AuthorEmail);
        var question = await FindQuestion(questionId);

        var answer = new Answer() 
        {
            AuthorId = user.Id, 
            Content = answerDto.Content,
            QuestionId=question.Id
        };

        _db.Answers.Add(answer);

        await _db.SaveChangesAsync();

        return _mapper.Map<PostDto>(answer);
    }

    private async Task<Question> FindQuestion(long id)
    {
        var user = await _db.Questions.FirstOrDefaultAsync(q => q.Id == id);
        if (user == null)
        {
            throw new Exception("Question doesn't exist!");
        }
        else
        {
            return user;
        }
    }
}