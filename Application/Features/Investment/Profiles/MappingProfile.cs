using Application.Features.Investment.Commands.Create;
using Application.Features.Investment.Commands.Delete;
using Application.Features.Investment.Commands.Update;
using AutoMapper;
using Domain.Enums;

namespace Application.Features.Investment.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Investment, CreateInvestmentCommand>().ReverseMap()
            .ForMember(destinationMember: dest => dest.InvestmentType, memberOptions: opt => opt.MapFrom(src => Enum.Parse<InvestmentType>(src.InvestmentType.ToString())));
        CreateMap<Domain.Entities.Investment, CreateInvestmentResponse>().ReverseMap();

        CreateMap<Domain.Entities.Investment, UpdateInvestmentCommand>().ReverseMap()
            .ForMember(destinationMember: dest => dest.InvestmentType, memberOptions: opt => opt.MapFrom(src => Enum.Parse<InvestmentType>(src.InvestmentType.ToString())));
        CreateMap<Domain.Entities.Investment, UpdateInvestmentResponse>().ReverseMap();
            
        CreateMap<Domain.Entities.Investment, DeleteInvestmentResponse>().ReverseMap();

        CreateMap<Domain.Entities.Investment, Queries.GetById.GetInvestmentByIdResponse>().ForMember(destinationMember: dest => dest.PortfolioName, memberOptions: opt => opt.MapFrom(src => src.Portfolio.Name)).ReverseMap();

        CreateMap<Domain.Entities.Investment, Domain.Entities.Investment>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.PortfolioId, opt => opt.Ignore());
    }
}