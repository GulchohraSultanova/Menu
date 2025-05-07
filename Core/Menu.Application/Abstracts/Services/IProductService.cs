using Menu.Application.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Application.Abstracts.Services
{
    public interface IProductService
    {
        Task<ProductDto> AddProductAsync(CreateProductDto createProductDto);
        Task<ProductDto?> GetProductAsync(string productId);
        Task<List<ProductDto>> GetAllProductsAsync();
  
        Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string productId);
    }
}
