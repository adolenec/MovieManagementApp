using System;
using api.Dtos;

namespace api.Interfaces
{
	public interface ICategoryRepository
	{
        Task<PagedList<CategoryDto>> GetCategoriesAsync(int page, int pageSize, string? searchTerm);
        Task<CategoryDetailsDto?> GetCategoryAsync(int id);
        Task<CategoryDetailsDto> AddCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDetailsDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, int id);
        Task DeleteCategoryAsync(int id);
    }
}

