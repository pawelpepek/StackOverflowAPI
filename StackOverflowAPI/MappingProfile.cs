using AutoMapper;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;

namespace StackOverflowAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>();
    }
}
