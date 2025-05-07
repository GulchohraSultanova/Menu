// CategoryProfile.cs
using AutoMapper;
using Menu.Domain.Entities;
using Menu.Application.Dtos.Categorys;

namespace Menu.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Entity → Read-only DTO
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.Id.ToString()));
            // :contentReference[oaicite:0]{index=0}:contentReference[oaicite:1]{index=1}

            // CreateDto → Entity
            CreateMap<CreateCategoryDto, Category>()
                // burada IFormFile → string xəritələnməsini service səviyyəsində edəcəksiniz
                .ForMember(dest => dest.CategoryImage, opt => opt.Ignore());
            // :contentReference[oaicite:2]{index=2}:contentReference[oaicite:3]{index=3}

            // UpdateDto → Entity (yalnız non-null sahələri yeniləsin)
            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryImage, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (src, dest, srcMember) => srcMember != null
                ));
            // :contentReference[oaicite:4]{index=4}:contentReference[oaicite:5]{index=5}
        }
    }
}
