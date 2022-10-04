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

    public async Task<MessageDto> AddChildMessage<TRoot, TChild>(long rootId, CreateMessageDto messageDto)
        where TRoot : Message
        where TChild : Message, new()
    {
        var user = await _userService.FindUser(messageDto.AuthorEmail);
        var rootMessage = await _finderService.FindEntity<TRoot>(rootId);

        var childMessage = new TChild()
        {
            AuthorId = user.Id,
            Content = messageDto.Content,
        };

        if(!SetPropertyValue(rootMessage.GetType().Name, childMessage, rootMessage.Id))
        {
            var parentClasses = typeof(MessageService).Assembly.GetExportedTypes()
                                                               .Where(t => typeof(TRoot)
                                                               .IsSubclassOf(t))
                                                               .ToList();

            var i = 0;
            while(i< parentClasses.Count)
            {
                if (SetPropertyValue(parentClasses[i++].Name,childMessage, rootMessage.Id))
                {
                    i = parentClasses.Count;
                }
            }
        }

        await _db.Set<TChild>().AddAsync(childMessage);

        await _db.SaveChangesAsync();

        return _mapper.Map<MessageDto>(childMessage);
    }

    private static bool SetPropertyValue(string typeName, object obj,long value)
    {
        var propertyInfo = obj.GetType().GetProperty($"{typeName}Id");
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(obj, value);
            return true;
        }
        else
        {
            return false;
        }

    }
}
