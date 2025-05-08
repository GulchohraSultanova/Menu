using AutoMapper;
using Menu.Domain.Entities;
using Menu.Application.Dtos.Categorys;

namespace Menu.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Entity → Read-only DTO (includes nested Products and SubCategories)
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Products,
                           opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.SubCategories,
                           opt => opt.MapFrom(src => src.SubCategories));

            // CreateDto → Entity
            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.CategoryImage, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCategoryId,
                           opt => opt.MapFrom(src =>
                               string.IsNullOrEmpty(src.ParentCategoryId) ? (Guid?)null : Guid.Parse(src.ParentCategoryId)));

            // UpdateDto → Entity (only non-null fields)
            CreateMap<UpdateCategoryDto, Category>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.CategoryImage, opt => opt.Ignore())
           .ForMember(dest => dest.ParentCategoryId, opt =>
           {
               // only map if the DTO supplied a non-null string
               opt.PreCondition(src => !string.IsNullOrEmpty(src.ParentCategoryId));
               // then parse it
               opt.MapFrom(src => Guid.Parse(src.ParentCategoryId!));
           })
           // everything else: only map non-null DTO properties
           .ForAllMembers(opt =>
               opt.Condition((src, dest, srcMember) => srcMember != null)
           );
        }
    }
}
