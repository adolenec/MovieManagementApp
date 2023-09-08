using System;
namespace api.Dtos
{
   public record MovieDetailsDto(int Id, string Name, string Description, double Rating, int Duration, DropdownDto Director,
      DropdownDto Category, bool IsFavourite, bool IsOnWatchlist, bool Watched);
}

