using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Entities;
using StackOverflowAPI.Interfaces;

namespace StackOverflowAPI.Services;

public class MessageFinderService : IMessageFinderService
{
    private readonly StackOverflowDbContext _db;

    public MessageFinderService(StackOverflowDbContext db)
    {
        _db = db;
    }

    public async Task<TEntity> FindEntity<TEntity>(long id)
        where TEntity : Message
    {
        var entity = await _db.Set<TEntity>().FirstOrDefaultAsync(p => p.Id == id);
        if (entity == null)
        {
            throw new Exception($"{typeof(TEntity).Name} doesn't exist!");
        }
        else
        {
            return entity;
        }
    }
}
