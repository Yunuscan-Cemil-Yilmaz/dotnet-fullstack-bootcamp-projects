using AutoMapper;
using PollCraft.DTOs;
using PollCraft.Models;

namespace PollCraft.Mappings;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequestDto, User>();
    }
}