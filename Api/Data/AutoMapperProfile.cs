using Api.Dto;
using Api.Models;
using AutoMapper;

namespace Api.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapper fra User til UserDto, og fra UserDto til User.
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}