using System;
namespace api.Dtos
{
    public record CreateMovieDto(string Name, string Description, double Rating, int Duration, int DirectorId,
      int CategoryId, bool? IsFavourite = false, bool? IsOnWatchlist = false, bool? Watched = false);
}

