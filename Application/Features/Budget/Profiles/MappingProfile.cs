using Application.Features.Budget.Commands.Create;
using Application.Features.Budget.Commands.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Budget.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Budget, CreateBudgetCommand>().ReverseMap();
        CreateMap<Domain.Entities.Budget, CreateBudgetResponse>().ReverseMap();

        CreateMap<Domain.Entities.Budget, UpdateBudgetCommand>().ReverseMap();
        CreateMap<Domain.Entities.Budget, UpdateBudgetResponse>().ReverseMap();

        CreateMap<Domain.Entities.Budget, Domain.Entities.Budget>()
            .ForMember( destinationMember: dest => dest.CreatedDate, memberOptions: opt => opt.Ignore())
            .ForMember( destinationMember: dest => dest.UpdatedDate, memberOptions: opt => opt.Ignore())
            .ForMember( destinationMember: dest => dest.DeletedDate, memberOptions: opt => opt.Ignore())
            .ForMember( destinationMember: dest => dest.AppUserId, memberOptions: opt => opt.Ignore());
    }
}