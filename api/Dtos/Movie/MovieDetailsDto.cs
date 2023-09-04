using System;
namespace api.Dtos
{
   public record MovieDetailsDto(int Id, string Name, string Description, double Rating, int Duration, string Director,
      string Category, bool IsFavourite, bool IsOnWatchlist, bool Watched);
}

