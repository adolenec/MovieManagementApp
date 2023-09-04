using System;
namespace api.Dtos
{
    public record UpdateMovieDto(string Name, string Description, double Rating, int Duration, int DirectorId, int CategoryId);
}

