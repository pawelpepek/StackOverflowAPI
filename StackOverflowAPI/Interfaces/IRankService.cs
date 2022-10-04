using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;

namespace StackOverflowAPI.Interfaces;

public interface IRankService
{
    Task<int> Vote<TEntity>(VoteDto dto) where TEntity : Post;
}