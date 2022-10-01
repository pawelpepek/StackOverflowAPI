using StackOverflowAPI.Entities;

namespace StackOverflowAPI.Interfaces;

public interface IMessageFinderService
{
    Task<TEntity> FindEntity<TEntity>(long id) where TEntity : Message;
}
