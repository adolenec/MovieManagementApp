using System;
namespace api.Dtos
{
    public record CreateDirectorDto(string FirstName, string LastName, DateTime DateOfBirth, string Description);
}

