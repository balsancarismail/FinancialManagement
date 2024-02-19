using Application.Features.Budget.Commands.Create;
using Application.Features.Budget.Commands.Update;
using Application.Features.Budget.Queries.GetById;
using Application.Features.Budget.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;

namespace Application.Features.Budget.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Budget, CreateBudgetCommand>().ReverseMap();
        CreateMap<Domain.Entities.Budget, CreateBudgetResponse>().ReverseMap();

        CreateMap<Domain.Entities.Budget, UpdateBudgetCommand>().ReverseMap();
        CreateMap<Domain.Entities.Budget, UpdateBudgetResponse>().ReverseMap();

        CreateMap<Domain.Entities.Budget, GetByIdBudgetResponse>()
            .ForMember(destinationMember: dest => dest.UserName, memberOptions: opt => opt.MapFrom(src => $"{src.AppUser.FirstName} {src.AppUser.LastName}")).ReverseMap();

        CreateMap<Domain.Entities.Budget, GetListBudgetListItemDto>()
            .ForMember(destinationMember: dest => dest.UserName, memberOptions: opt => opt.MapFrom(src => $"{src.AppUser.FirstName} {src.AppUser.LastName}")).ReverseMap();

        CreateMap<Paginate<Domain.Entities.Budget>, GetListResponse<GetListBudgetListItemDto>>().ReverseMap();
        

        CreateMap<Domain.Entities.Budget, Domain.Entities.Budget>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.AppUserId, opt => opt.Ignore());
    }
}