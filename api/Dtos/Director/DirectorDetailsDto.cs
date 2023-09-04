using System;
namespace api.Dtos
{
    public record DirectorDetailsDto(int Id, string FirstName, string LastName, string Description, DateTime DateOfBirth, IList<MovieDto> Movies);
}

