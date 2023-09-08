using System;
using api.Dtos;

namespace api.Interfaces
{
    public interface IDirectorRepository
    {
        Task<PagedList<DirectorDto>> GetDirectorsAsync(int page, int pageSize, string? searchTerm);
        Task<ICollection<DropdownDto>> GetDropdownDirectorsAsync(string? searchTerm);
        Task<DirectorDetailsDto?> GetDirectorAsync(int id);
        Task<DirectorDetailsDto> AddDirectorAsync(CreateDirectorDto createDirectorDto);
        Task<DirectorDetailsDto> UpdateDirectorAsync(UpdateDirectorDto updateDirectorDto, int id);
        Task DeleteDirectorAsync(int id);
    }
}

