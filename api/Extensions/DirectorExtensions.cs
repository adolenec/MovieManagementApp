using System;
using api.Dtos;
using api.Interfaces;

namespace api.Extensions
{
	public static class DirectorExtensions
	{
        public static void MapDirectorEndpoints(this WebApplication app)
        {
            app.MapGet("/directors", (IDirectorRepository repository, int page, int pageSize, string? searchTerm) =>
                repository.GetDirectorsAsync(page, pageSize, searchTerm));

            app.MapGet("directors/{id}", async (IDirectorRepository repository, int id) =>
            {
                var director = await repository.GetDirectorAsync(id);

                if (director is null)
                    return Results.Problem($"Director with Id {id} was not found", statusCode: 404);
                return Results.Ok(director);
            });

            app.MapPost("/directors", async (IDirectorRepository repository, CreateDirectorDto createDirectorDto) =>
            {
                var director = await repository.AddDirectorAsync(createDirectorDto);
                return Results.Created($"/directors/{director.Id}", director);
            });

            app.MapPut("/directors/{id}", async (UpdateDirectorDto directorDto, int id, IDirectorRepository repository) =>
            {
                var director = await repository.GetDirectorAsync(id);
                if (director is null)
                    return Results.Problem($"Director with id {id} was not found", statusCode: 404);

                var updatedDirector = await repository.UpdateDirectorAsync(directorDto, id);
                return Results.Ok(updatedDirector);
            });

            app.MapDelete("/directors/{id}", async (IDirectorRepository repository, int id) =>
            {
                var director = await repository.GetDirectorAsync(id);
                if (director is null)
                    return Results.Problem($"Director with id {id} was not found", statusCode: 404);
                await repository.DeleteDirectorAsync(id);
                return Results.Ok();
            });
        }
    }
}

