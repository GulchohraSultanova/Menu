using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Application.Abstracts.Services;
using Menu.Application.Dtos.Categorys;
using Menu.Application.GlobalAppException;

namespace Menu.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategorysController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // ✅ Yeni kategori yarat
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto dto)
        {
            try
            {
                var category = await _categoryService.AddCategoryAsync(dto);
                return StatusCode(StatusCodes.Status201Created, new { StatusCode = 201, Data = category });
            }
            catch (GlobalAppException ex)
            {
                return BadRequest(new { StatusCode = 400, Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Error = $"Xəta baş verdi: {ex.Message}" });
            }
        }

        // ✅ Kategoriyanı ID ilə gətir (Products daxil olmaqla)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            try
            {
                var category = await _categoryService.GetCategoryAsync(id);
                return Ok(new { StatusCode = 200, Data = category });
            }
            catch (GlobalAppException ex)
            {
                return BadRequest(new { StatusCode = 400, Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Error = $"Xəta baş verdi: {ex.Message}" });
            }
        }

        // ✅ Bütün kateqoriyaları gətir (Products daxil olmaqla)
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var list = await _categoryService.GetAllCategorysAsync();
                return Ok(new { StatusCode = 200, Data = list });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Error = $"Xəta baş verdi: {ex.Message}" });
            }
        }

        // ✅ Kategoriyanı yenilə
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryDto dto)
        {
            try
            {
                var updated = await _categoryService.UpdateCategoryAsync(dto);
                return Ok(new { StatusCode = 200, Message = "Kategoriya uğurla yeniləndi!", Data = updated });
            }
            catch (GlobalAppException ex)
            {
                return BadRequest(new { StatusCode = 400, Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Error = $"Xəta baş verdi: {ex.Message}" });
            }
        }

        // ✅ Kategoriyanı sil
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok(new { StatusCode = 200, Message = "Kategoriya uğurla silindi!" });
            }
            catch (GlobalAppException ex)
            {
                return BadRequest(new { StatusCode = 400, Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Error = $"Xəta baş verdi: {ex.Message}" });
            }
        }
    }
}
