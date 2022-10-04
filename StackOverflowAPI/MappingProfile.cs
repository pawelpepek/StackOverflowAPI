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

        CreateMap<Question, QuestionDto>()
            .ForMember(q => q.Tags, d => d.MapFrom(qq => qq.Tags.Select(t => t.Text).ToList()));
        CreateMap<Post, PostDto>();
        CreateMap<Message, MessageDto>();
    }
}
