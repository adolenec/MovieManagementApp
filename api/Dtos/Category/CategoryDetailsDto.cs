using System;
namespace api.Dtos
{
    public record CategoryDetailsDto(int Id, string Name, string Description, IList<MovieDto> Movies);
}

