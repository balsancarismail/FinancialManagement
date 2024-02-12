using Application.Features.Investment.Commands.Create;
using Application.Features.Investment.Commands.Update;
using AutoMapper;

namespace Application.Features.Investment.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Investment, CreateInvestmentCommand>().ReverseMap();
        CreateMap<Domain.Entities.Investment, CreateInvestmentResponse>().ReverseMap();

        CreateMap<Domain.Entities.Investment, UpdateInvestmentCommand>().ReverseMap();
        CreateMap<Domain.Entities.Investment, UpdateInvestmentResponse>().ReverseMap();
        //
        //CreateMap<Domain.Entities.Investment, DeleteInvestmentPortfolioResponse>().ReverseMap();

        CreateMap<Domain.Entities.Investment, Domain.Entities.Investment>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.PortfolioId, opt => opt.Ignore());
    }
}