using AutoMapper;

namespace Application.Features.FinancialTransaction.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateMap<Domain.Entities.Budget, CreateBudgetCommand>().ReverseMap();
        //CreateMap<Domain.Entities.Budget, CreateBudgetResponse>().ReverseMap();
        //
        //CreateMap<Domain.Entities.Budget, UpdateBudgetCommand>().ReverseMap();
        //CreateMap<Domain.Entities.Budget, UpdateBudgetResponse>().ReverseMap();

        CreateMap<Domain.Entities.FinancialTransaction, Domain.Entities.FinancialTransaction>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.AppUserId, opt => opt.Ignore())
            ;
    }
}