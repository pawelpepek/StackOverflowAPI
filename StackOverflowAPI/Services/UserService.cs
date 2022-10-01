using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;
using StackOverflowAPI.Interfaces;

namespace StackOverflowAPI.Services;

public class UserService : IUserService
{
    private readonly StackOverflowDbContext _db;
    private readonly IMapper _mapper;

    public UserService(StackOverflowDbContext db, IMapper mapper)
    {
        this._db = db;
        this._mapper = mapper;
    }

    public async Task<int> AddNewUser(UserDto dto)
    {
        if (UserExists(dto.Email))
        {
            throw new Exception("Email exists in database!");
        }
        else
        {
            dto.Email = dto.Email.Trim();
            var user = _mapper.Map<User>(dto);

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user.Id;
        }
    }

    public List<UserDto> GetUsers()
    {
        return _db.Users.AsNoTracking()
               .OrderBy(u => u.Name)
               .Select(_mapper.Map<UserDto>)
               .ToList();
    }

    public async Task<UserDto> GetUser(string email)
    {
        return _mapper.Map<UserDto>(await FindUser(email));
    }

    public async Task UpdateUser(UserDto dto)
    {
        var user = await FindUser(dto.Email);

        user.Name = dto.Name;

        await _db.SaveChangesAsync();
    }

    public async Task<User> FindUser(string email)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower().Trim());
        if (user == null)
        {
            throw new Exception("User doesn't exist!");
        }
        else
        {
            return user;
        }
    }
 
    private bool UserExists(string email)
        => _db.Users.Any(u => u.Email.ToLower() == email.ToLower().Trim());


}
