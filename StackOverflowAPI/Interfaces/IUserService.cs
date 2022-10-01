using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;

namespace StackOverflowAPI.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUser(string email);
    Task<int> AddNewUser(UserDto dto);
    List<UserDto> GetUsers();
    Task UpdateUser(UserDto dto);
    Task<User> FindUser(string email);
}
