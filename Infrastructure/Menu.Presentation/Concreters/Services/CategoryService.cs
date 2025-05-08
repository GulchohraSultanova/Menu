using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Menu.Application.Abstracts.Services;

using Menu.Application.Dtos.Categorys;
using Menu.Application.GlobalAppException;
using Menu.Domain.Entities;
using Menu.Application.Absrtacts.Repositories.Categorys;
using Menu.Application.Absrtacts.Services;
using System.Threading;

namespace Menu.Presentation.Concreters.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryReadRepository _categoryReadRepo;
        private readonly ICategoryWriteRepository _categoryWriteRepo;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryReadRepository categoryReadRepo,
            ICategoryWriteRepository categoryWriteRepo,
            IFileService fileService,
            IMapper mapper)
        {
            _categoryReadRepo = categoryReadRepo;
            _categoryWriteRepo = categoryWriteRepo;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<CategoryDto> AddCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            if (dto.CategoryImage != null)
                category.CategoryImage = await _fileService.UploadFile(dto.CategoryImage, "category_images");

            await _categoryWriteRepo.AddAsync(category);
            await _categoryWriteRepo.CommitAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> GetCategoryAsync(string id)
        {
            if (!Guid.TryParse(id, out var gid))
                throw new GlobalAppException("Yanlış Category ID!");

            var cat = await _categoryReadRepo.GetAsync(
                c => !c.IsDeleted && c.Id == gid,
                q => q.Include(c => c.Products)                      // ana kategorinin ürünleri
        .Include(c => c.SubCategories)                 // alt kategoriler
            .ThenInclude(sc => sc.Products)
            );
            if (cat == null) return null;

            // if this is a sub-category, clear its SubCategories list
            if (cat.ParentCategoryId != null)
                cat.SubCategories = null;

            return _mapper.Map<CategoryDto>(cat);
        }

        public async Task<List<CategoryDto>> GetAllCategorysAsync()
        {
            var roots = await _categoryReadRepo.GetAllAsync(
                c => !c.IsDeleted && c.ParentCategoryId == null,
                q => q.Include(c => c.Products)                      // ana kategorinin ürünleri
        .Include(c => c.SubCategories)                 // alt kategoriler
            .ThenInclude(sc => sc.Products)
            );
            return _mapper.Map<List<CategoryDto>>(roots);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Id) || !Guid.TryParse(dto.Id, out var cid))
                throw new GlobalAppException("Yanlış Category ID!");

            var category = await _categoryReadRepo.GetByIdAsync(dto.Id)
                ?? throw new GlobalAppException("Category tapılmadı!");

            _mapper.Map(dto, category);

            if (dto.CategoryImage != null)
            {
                if (!string.IsNullOrEmpty(category.CategoryImage))
                    await _fileService.DeleteFile("category_images", category.CategoryImage);

                category.CategoryImage = await _fileService.UploadFile(dto.CategoryImage, "category_images");
            }

            await _categoryWriteRepo.UpdateAsync(category);
            await _categoryWriteRepo.CommitAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            if (!Guid.TryParse(categoryId, out var cid))
                throw new GlobalAppException("Yanlış Category ID!");

            var category = await _categoryReadRepo.GetByIdAsync(categoryId)
                         ?? throw new GlobalAppException("Category tapılmadı!");

            if (!string.IsNullOrEmpty(category.CategoryImage))
                await _fileService.DeleteFile("category_images", category.CategoryImage);

            category.IsDeleted = true;
            await _categoryWriteRepo.UpdateAsync(category);
            await _categoryWriteRepo.CommitAsync();
        }

    }
}
