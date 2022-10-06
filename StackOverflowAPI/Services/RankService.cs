using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;
using StackOverflowAPI.Interfaces;

namespace StackOverflowAPI.Services;

public class RankService : IRankService
{
    private readonly StackOverflowDbContext _db;
    private readonly IUserService _userService;

    public RankService(StackOverflowDbContext db, IUserService userService)
    {
        _db = db;
        _userService = userService;
    }

    public async Task<int> Vote<TEntity>(VoteDto dto) where TEntity : Post
    {
        var user = await _userService.FindUser(dto.UserEmail);

        var post = await _db.Set<TEntity>().Include(p => p.Votes)
                                           .FirstOrDefaultAsync(p => p.Id == dto.PostId);
        if (post == null)
        {
            throw new Exception($"{typeof(TEntity).Name} doesn't exist!");
        }
        else
        {
            if (post.AuthorId == user.Id)
            {
                throw new Exception("You can't rate your post");
            }

            var lastVote = post.Votes.FirstOrDefault(v => v.UserId == user.Id);

            if (lastVote != null)
            {
                if (lastVote.Like == dto.Like)
                {
                    throw new Exception("Can't rate a post a second time");
                }
                else
                {
                    _db.Votes.Remove(lastVote);
                }
            }
            else
            {
                _db.Votes.Add(new Vote()
                {
                    Like = dto.Like,
                    PostId = dto.PostId,
                    UserId = user.Id
                });
            }

            await _db.SaveChangesAsync();

            return post.Rank;
        }
    }
}
