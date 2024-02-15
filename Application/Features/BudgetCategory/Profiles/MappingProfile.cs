
using Application.Features.BudgetCategory.Queries.GetById;
using AutoMapper;

namespace Application.Features.BudgetCategory.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.BudgetCategory, GetBudgetCategoryByIdResponse>().ForMember(destinationMember: dest => dest.CategoryName, memberOptions: opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
    }
}