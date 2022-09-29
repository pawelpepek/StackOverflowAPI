using AutoMapper;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;

namespace StackOverflowAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();

        CreateMap<Question, QuestionDto>();
        CreateMap<Post, PostDto>();
        CreateMap<Message, MessageDto>();


    }
}
