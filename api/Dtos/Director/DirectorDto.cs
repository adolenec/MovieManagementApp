using System;
namespace api.Dtos
{
    public record DirectorDto(int Id, string FirstName, string LastName, DateTime DateOfBirth, string Description);
}

