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

        var post = await _db.Set<TEntity>().Include(p => p.UserLikes)
                                           .Include(p => p.UserDislikes)
                                           .FirstOrDefaultAsync(p => p.Id == dto.PostId);
        if (post == null)
        {
            throw new Exception($"{typeof(TEntity).Name} doesn't exist!");
        }
        else
        {
            var listToAdd = dto.Like ? post.UserLikes : post.UserDislikes;
            var listToRemove = dto.Like ? post.UserDislikes : post.UserLikes;

            if(post.AuthorId==user.Id)
            {
                throw new Exception("You can't rate your post");
            }

            if (listToAdd.Contains(user))
            {
                throw new Exception("Can't rate a post a second time");
            }
            else
            {
                if (listToRemove.Contains(user))
                {
                    listToRemove.Remove(user);
                }
                else
                {
                    listToAdd.Add(user);
                }

                await _db.SaveChangesAsync();

                return post.Rank;
            }
        }
    }
}
