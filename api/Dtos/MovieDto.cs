using System;
using api.Domain.Entities;

namespace api.Dtos
{
    public record MovieDto(int Id, string Name, double Rating, int Duration, string Director,
        string Category, bool IsFavourite, bool IsOnWatchlist, bool Watched);
}

