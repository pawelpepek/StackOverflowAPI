using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;

namespace StackOverflowAPI.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> AddChildMessage<TRoot, TChild>(long rootId, CreateMessageDto answerDto)
            where TRoot : Message
            where TChild : Message, new();
    }
}