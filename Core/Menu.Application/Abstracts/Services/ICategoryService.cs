using Menu.Application.Dtos.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Application.Abstracts.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> AddCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto?> GetCategoryAsync(string categoryId);
        Task<List<CategoryDto>> GetAllCategorysAsync();
        Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(string categoryId);

       
    }
}
