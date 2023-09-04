using System;
using api.Dtos;

namespace api.Interfaces
{
	public interface IMovieRepository
	{
        Task<PagedList<MovieDto>> GetMoviesAsync(int page, int pageSize, string? searchTerm);
        Task<MovieDetailsDto?> GetMovieAsync(int id);
        Task UpdateFavouriteState(int id, bool isFavourite);
        Task UpdateWatchedState(int id, bool isWatched);
        Task UpdateWatchlistState(int id, bool isOnWatchlist);
    }
}

