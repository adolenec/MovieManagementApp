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
	public class DirectorRepository : IDirectorRepository
	{
        private readonly MovieManagementContext _context;
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public DirectorRepository(MovieManagementContext context, IConfigurationProvider configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<PagedList<DirectorDto>> GetDirectorsAsync(int page, int pageSize, string? searchTerm)
        {
            var query = _context.Directors.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(d => (d.FirstName.ToLower() + " " + d.LastName).Contains(searchTerm));

            }

            var totalCount = await query.CountAsync();

            var directors = await query.Skip((page - 1) * pageSize).Take(pageSize)
                .ProjectTo<DirectorDto>(_configuration).ToListAsync();

            var pagedList = new PagedList<DirectorDto>(directors, totalCount);

            return pagedList;
        }

        public async Task<DirectorDetailsDto?> GetDirectorAsync(int id)
        {
            var director = await _context.Directors
                .Include(d => d.Movies)
                .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(d => d.Id == id);


            if (director is null) return null;

            return _mapper.Map<DirectorDetailsDto>(director);
        }

        public async Task<DirectorDetailsDto> AddDirectorAsync(CreateDirectorDto createDirectorDto)
        {
            var director = _mapper.Map<Director>(createDirectorDto);
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return _mapper.Map<DirectorDetailsDto>(director);
        }

        public async Task<DirectorDetailsDto> UpdateDirectorAsync(UpdateDirectorDto updateDirectorDto, int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director is null)
                throw new ArgumentException($"Error updating director");
            director.FirstName = updateDirectorDto.FirstName;
            director.LastName = updateDirectorDto.LastName;
            director.Description = updateDirectorDto.Description;
            director.DateOfBirth = updateDirectorDto.DateOfBirth;
            _context.Entry(director).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<DirectorDetailsDto>(director);
        }

        public async Task DeleteDirectorAsync(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director is null)
                throw new ArgumentException($"Error deleting director");
            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
        }
    }
}

