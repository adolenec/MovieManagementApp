using System;
using api.Domain;
using api.Domain.Entities;
using api.Dtos;
using api.Interfaces;
using api.Mappings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace api.Repositories
{
	public class MovieRepository : IMovieRepository
	{
        private readonly MovieManagementContext _context;
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MovieRepository(MovieManagementContext context, IConfigurationProvider configuration, IMapper mapper)
		{
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<PagedList<MovieDto>> GetMoviesAsync(int page, int pageSize, string? searchTerm)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(m => m.Name.ToLower().Contains(searchTerm) ||
                                         (m.Director.FirstName + " " + m.Director.LastName).ToLower().Contains(searchTerm) ||
                                         m.Category.Name.ToLower().Contains(searchTerm));

            }

            var totalCount = await query.CountAsync();

            var movies = await query.Skip((page - 1) * pageSize).Take(pageSize)
                .ProjectTo<MovieDto>(_configuration).ToListAsync();

            var pagedList = new PagedList<MovieDto>(movies, totalCount);

            return pagedList;
        }

        public async Task<ICollection<MovieDto>> GetFavouriteMoviesAsync()
        {

            return await _context.Movies.Where(m => m.IsFavourite)
                .ProjectTo<MovieDto>(_configuration).ToListAsync();

        }

        public async Task<ICollection<MovieDto>> GetWatchedMoviesAsync()
        {

            return await _context.Movies.Where(m => m.Watched)
                .ProjectTo<MovieDto>(_configuration).ToListAsync();

        }

        public async Task<ICollection<MovieDto>> GetWatchlistMoviesAsync()
        {

            return await _context.Movies.Where(m => m.IsOnWatchlist)
                .ProjectTo<MovieDto>(_configuration).ToListAsync();

        }

        public async Task<MovieDetailsDto?> GetMovieAsync(int id)
        {
            var movie = await _context.Movies.Include(m => m.Category).Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null) return null;

            return _mapper.Map<MovieDetailsDto>(movie);
        }

        public async Task<MovieDetailsDto> AddMovieAsync(CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return _mapper.Map<MovieDetailsDto>(movie);
        }

        public async Task<MovieDetailsDto> UpdateMovieAsync(UpdateMovieDto updateMovieDto, int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null)
                throw new ArgumentException($"Error updating movie");
            movie.Name = updateMovieDto.Name;
            movie.Description = updateMovieDto.Description;
            movie.Rating = updateMovieDto.Rating;
            movie.Duration = updateMovieDto.Duration;
            movie.CategoryId = updateMovieDto.CategoryId;
            movie.DirectorId = updateMovieDto.DirectorId;
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<MovieDetailsDto>(movie);
            
        }

        public async Task UpdateFavouriteState(int id, bool isFavourite)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null)
                throw new ArgumentException("$Error updating movie favourite state");
            movie.IsFavourite = isFavourite;
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWatchedState(int id, bool isWatched)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null)
                throw new ArgumentException("$Error updating movie watched state");
            movie.Watched = isWatched;
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWatchlistState(int id, bool isOnWatchlist)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null)
                throw new ArgumentException("$Error updating movie isOnWatchlist state");
            movie.IsOnWatchlist = isOnWatchlist;
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null)
                throw new ArgumentException($"Error deleting movie");
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
    }
}

