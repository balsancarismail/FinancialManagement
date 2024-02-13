using Application.Features.FinancialTransaction.Commands.Create;
using Application.Features.FinancialTransaction.Commands.Update;
using AutoMapper;

namespace Application.Features.FinancialTransaction.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.FinancialTransaction, CreateFinancialTransactionCommand>().ReverseMap();
        CreateMap<Domain.Entities.FinancialTransaction, CreateFinancialTransactionResponse>().ReverseMap();
        
        CreateMap<Domain.Entities.FinancialTransaction, UpdateFinancialTransactionCommand>().ReverseMap();
        CreateMap<Domain.Entities.FinancialTransaction, UpdateFinancialTransactionResponse>().ReverseMap();

        CreateMap<Domain.Entities.FinancialTransaction, Domain.Entities.FinancialTransaction>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.AppUserId, opt => opt.Ignore())
            ;
    }
}