using Application.Features.BudgetCategory.Queries.GetById;
using AutoMapper;

namespace Application.Features.BudgetCategory.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.BudgetCategory, GetBudgetCategoryByIdResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
    }
}