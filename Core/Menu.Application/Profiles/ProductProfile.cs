// ProductProfile.cs
using AutoMapper;
using Menu.Domain.Entities;
using Menu.Application.Dtos.Products;

namespace Menu.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Entity → Read-only DTO
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.CategoryId,
                           opt => opt.MapFrom(src => src.CategoryId.ToString()));
            // :contentReference[oaicite:6]{index=6}:contentReference[oaicite:7]{index=7}

            // CreateDto → Entity
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.CategoryId,
                           opt => opt.MapFrom(src => Guid.Parse(src.CategoryId)));
            // :contentReference[oaicite:8]{index=8}:contentReference[oaicite:9]{index=9}

            // UpdateDto → Entity (yalnız non-null sahələri yeniləsin)
            CreateMap<UpdateProductDto, Product>().
                
                ForMember(dest => dest.CategoryId, opt =>
                    opt.PreCondition(src => src.CategoryId != null))
                .ForMember(dest => dest.CategoryId, opt =>
                    opt.MapFrom(src => Guid.Parse(src.CategoryId!))).ForAllMembers(opt => opt.Condition(
                    (src, dest, srcMember) => srcMember != null
                ));
            // :contentReference[oaicite:10]{index=10}:contentReference[oaicite:11]{index=11}
        }
    }
}
