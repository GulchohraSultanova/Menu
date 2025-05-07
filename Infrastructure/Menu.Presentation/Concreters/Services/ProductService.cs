// Persistence/Concreters/Services/ProductService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Menu.Application.Abstracts.Services;
using Menu.Application.Abstracts.Repositories.Products;
using Menu.Application.Dtos.Products;
using Menu.Application.GlobalAppException;
using Menu.Domain.Entities;

namespace Menu.Presentation.Concreters.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepo;
        private readonly IProductWriteRepository _productWriteRepo;
        private readonly IMapper _mapper;

        public ProductService(
            IProductReadRepository productReadRepo,
            IProductWriteRepository productWriteRepo,
            IMapper mapper)
        {
            _productReadRepo = productReadRepo;
            _productWriteRepo = productWriteRepo;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _productWriteRepo.AddAsync(product);
            await _productWriteRepo.CommitAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> GetProductAsync(string productId)
        {
            if (!Guid.TryParse(productId, out var pid))
                throw new GlobalAppException("Yanlış Product ID!");

            var product = await _productReadRepo.GetAsync(
                p => !p.IsDeleted && p.Id == pid
            );

            return product == null
                ? null
                : _mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var list = await _productReadRepo.GetAllAsync(
                p => !p.IsDeleted
            );
            return _mapper.Map<List<ProductDto>>(list);
        }


        public async Task<ProductDto> UpdateProductAsync(UpdateProductDto dto)
        {
            // Ensure UpdateProductDto has Id
            if (string.IsNullOrWhiteSpace(dto.Id) || !Guid.TryParse(dto.Id, out var pid))
                throw new GlobalAppException("Yanlış Product ID!");

            var product = await _productReadRepo.GetByIdAsync(dto.Id)
                          ?? throw new GlobalAppException("Product tapılmadı!");

            _mapper.Map(dto, product);

            if (!string.IsNullOrEmpty(dto.CategoryId))
            {
                if (!Guid.TryParse(dto.CategoryId, out var newCid))
                    throw new GlobalAppException("Yanlış Category ID!");
                product.CategoryId = newCid;
            }

            await _productWriteRepo.UpdateAsync(product);
            await _productWriteRepo.CommitAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProductAsync(string productId)
        {
            if (!Guid.TryParse(productId, out var pid))
                throw new GlobalAppException("Yanlış Product ID!");

            var product = await _productReadRepo.GetByIdAsync(productId)
                          ?? throw new GlobalAppException("Product tapılmadı!");

            product.IsDeleted = true;
            await _productWriteRepo.UpdateAsync(product);
            await _productWriteRepo.CommitAsync();
        }
    }
}