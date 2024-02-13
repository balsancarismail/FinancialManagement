using Application.Features.Category.Commands.Create;
using Application.Features.Category.Commands.Delete;
using Application.Features.Category.Commands.Update;
using AutoMapper;

namespace Application.Features.Category.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Budget, CreateCategoryCommand>().ReverseMap();
        CreateMap<Domain.Entities.Budget, CreateCategoryResponse>().ReverseMap();

        CreateMap<Domain.Entities.Budget, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Domain.Entities.Budget, UpdateCategoryResponse>().ReverseMap();

        CreateMap<Domain.Entities.Budget, DeleteCategoryResponse>().ReverseMap();


        CreateMap<Domain.Entities.Category, Domain.Entities.Category>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore());
    }
}