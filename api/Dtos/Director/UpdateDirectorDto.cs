using System;
namespace api.Dtos
{
    public record UpdateDirectorDto(string FirstName, string LastName, string Description, DateTime DateOfBirth);
}

