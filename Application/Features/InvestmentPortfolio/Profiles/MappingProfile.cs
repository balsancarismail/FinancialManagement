using Application.Features.InvestmentPortfolio.Commands.Create;
using Application.Features.InvestmentPortfolio.Commands.Delete;
using Application.Features.InvestmentPortfolio.Commands.Update;
using Application.Features.InvestmentPortfolio.Queries.GetById;
using Application.Features.InvestmentPortfolio.Queries.GetListInvestmentPortfolio;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;

namespace Application.Features.InvestmentPortfolio.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.InvestmentPortfolio, CreateInvestmentPortfolioCommand>().ReverseMap();
        CreateMap<Domain.Entities.InvestmentPortfolio, CreateInvestmentPortfolioResponse>().ReverseMap();

        CreateMap<Domain.Entities.InvestmentPortfolio, UpdateInvestmentPortfolioCommand>().ReverseMap();
        CreateMap<Domain.Entities.InvestmentPortfolio, UpdateInvestmentPortfolioResponse>().ReverseMap();

        CreateMap<Domain.Entities.InvestmentPortfolio, DeleteInvestmentPortfolioResponse>().ReverseMap();

        CreateMap<Domain.Entities.InvestmentPortfolio, GetInvestmentPortfolioByIdResponse>().ReverseMap();
        CreateMap<Domain.Entities.InvestmentPortfolio, GetListInvestmentPortfolioListItemDto>().ReverseMap();

        CreateMap<Paginate<Domain.Entities.InvestmentPortfolio>,
            GetListResponse<GetListInvestmentPortfolioListItemDto>>();

        CreateMap<Domain.Entities.InvestmentPortfolio, Domain.Entities.InvestmentPortfolio>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.AppUserId, opt => opt.Ignore());
    }
}