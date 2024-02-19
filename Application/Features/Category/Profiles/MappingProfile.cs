using Application.Features.Category.Commands.Create;
using Application.Features.Category.Commands.Delete;
using Application.Features.Category.Commands.Update;
using Application.Features.Category.Queries.GetById;
using Application.Features.Category.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Enums;

namespace Application.Features.Category.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Category, CreateCategoryCommand>().ReverseMap()
            .ForMember(dest => dest.CategoryType,
                opt => opt.MapFrom(src => Enum.Parse<CategoryType>(src.CategoryType.ToString())));
        CreateMap<CreateCategoryResponse, Domain.Entities.Category>().ReverseMap();

        CreateMap<Domain.Entities.Category, UpdateCategoryCommand>().ReverseMap()
            .ForMember(dest => dest.CategoryType,
                opt => opt.MapFrom(src => Enum.Parse<CategoryType>(src.CategoryType.ToString())));
        ;
        CreateMap<Domain.Entities.Category, UpdateCategoryResponse>().ReverseMap();

        CreateMap<Domain.Entities.Category, DeleteCategoryResponse>().ReverseMap();

        CreateMap<Domain.Entities.Category, GetCategoryByIdResponse>().ReverseMap();

        CreateMap<Domain.Entities.Category, GetListCategoryListItemDto>().ReverseMap();
        CreateMap<Paginate<Domain.Entities.Category>, GetListResponse<GetListCategoryListItemDto>>().ReverseMap();


        CreateMap<Domain.Entities.Category, Domain.Entities.Category>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore());
    }
}