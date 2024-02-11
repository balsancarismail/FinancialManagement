using Application.Features.Users.Commands.Create;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppUser, CreateUserCommand>().ReverseMap()
            .ForMember(destinationMember: x => x.UserName, memberOptions: opt => opt.MapFrom(src => src.Email));
        CreateMap<AppUser, CreatedUserResponse>().ReverseMap()
           
            .ForAllMembers(opt =>
                opt.Condition(src => src.Email is not null && src.FirstName is not null && src.LastName is not null));
    }
}