using System;
using api.Domain;
using api.Domain.Entities;
using api.Dtos;
using api.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace api.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly MovieManagementContext _context;
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public CategoryRepository(MovieManagementContext context, IConfigurationProvider configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<PagedList<CategoryDto>> GetCategoriesAsync(int page, int pageSize, string? searchTerm)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(m => m.Name.ToLower().Contains(searchTerm));

            }

            var totalCount = await query.CountAsync();

            var categories = await query.Skip((page - 1) * pageSize).Take(pageSize)
                .ProjectTo<CategoryDto>(_configuration).ToListAsync();

            var pagedList = new PagedList<CategoryDto>(categories, totalCount);

            return pagedList;
        }

        public async Task<CategoryDetailsDto?> GetCategoryAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Movies).ThenInclude(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (category is null) return null;

            return _mapper.Map<CategoryDetailsDto>(category);
        }

        public async Task<CategoryDetailsDto> AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDetailsDto>(category);
        }

        public async Task<CategoryDetailsDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                throw new ArgumentException($"Error updating category");
            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDetailsDto>(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                throw new ArgumentException($"Error deleting category");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}

