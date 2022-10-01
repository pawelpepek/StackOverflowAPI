using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;
using StackOverflowAPI.Interfaces;

namespace StackOverflowAPI.Services;

public class MessageService : IMessageService
{
    private readonly StackOverflowDbContext _db;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IMessageFinderService _finderService;

    public MessageService(StackOverflowDbContext db, IMapper mapper,
        IUserService userService, IMessageFinderService finderService)
    {
        this._db = db;
        this._mapper = mapper;
        this._userService = userService;
        this._finderService = finderService;
    }

    public async Task<MessageDto> AddChildMessage<TRoot, TChild>(long rootId, CreateMessageDto answerDto)
        where TRoot : Message
        where TChild : Message, new()
    {
        var user = await _userService.FindUser(answerDto.AuthorEmail);
        var rootMessage = await _finderService.FindEntity<TRoot>(rootId);

        var childMessage = new TChild()
        {
            AuthorId = user.Id,
            Content = answerDto.Content,
        };

        var propertyInfo = childMessage.GetType().GetProperty($"{typeof(TRoot).Name}Id");
        propertyInfo.SetValue(childMessage, rootMessage.Id);

        await _db.Set<TChild>().AddAsync(childMessage);

        await _db.SaveChangesAsync();

        return _mapper.Map<MessageDto>(childMessage);
    }
}
