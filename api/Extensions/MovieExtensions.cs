using System;
using api.Dtos;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Extensions
{
    public static class MovieExtensions
    {
        public static void MapMovieEndpoints(this WebApplication app)
        {
            app.MapGet("/movies", (IMovieRepository repository, int page, int pageSize, string? searchTerm) =>
                repository.GetMoviesAsync(page, pageSize, searchTerm));

            app.MapGet("movies/{id}", async (IMovieRepository repository, int id) =>
            {
                var movie = await repository.GetMovieAsync(id);

                if (movie is null)
                    return Results.Problem($"Movie with Id {id} was not found", statusCode: 404);
                return Results.Ok(movie);
            });

            app.MapPost("/movies", async (IMovieRepository repository, ICategoryRepository categoryRepository, IDirectorRepository directorRepository, CreateMovieDto createMovieDto) =>
            {
                var director = await directorRepository.GetDirectorAsync(createMovieDto.DirectorId);

                if (director is null)
                    return Results.Problem($"Director with id {createMovieDto.DirectorId} was not found", statusCode: 404);

                var category = await categoryRepository.GetCategoryAsync(createMovieDto.CategoryId);

                if (category is null)
                    return Results.Problem($"Category with id {createMovieDto.CategoryId} was not found", statusCode: 404);

                var movie = await repository.AddMovieAsync(createMovieDto);

                return Results.Created($"/movies/{movie.Id}", movie);
            });

            app.MapPut("/movies", async (IMovieRepository repository, ICategoryRepository categoryRepository, IDirectorRepository directorRepository, UpdateMovieDto updateMovieDto, int id) =>
            {
                var movie = await repository.GetMovieAsync(id);

                if (movie is null)
                    return Results.Problem($"Movie with id {id} was not found", statusCode: 404);

                var director = await directorRepository.GetDirectorAsync(updateMovieDto.DirectorId);

                if (director is null)
                    return Results.Problem($"Director with id {updateMovieDto.DirectorId} was not found", statusCode: 404);

                var category = await categoryRepository.GetCategoryAsync(updateMovieDto.CategoryId);

                if (category is null)
                    return Results.Problem($"Category with id {updateMovieDto.CategoryId} was not found", statusCode: 404);

                var updatedMovie = await repository.UpdateMovieAsync(updateMovieDto, id);
                return Results.Ok(updateMovieDto);
            });

            app.MapPut("/movies/favourite", async (IMovieRepository repository, int id, bool isFavourite) =>
            {
                var movie = await repository.GetMovieAsync(id);
                if (movie is null)
                    return Results.Problem($"Movie with id {id} was not found", statusCode: 404);

                await repository.UpdateFavouriteState(id, isFavourite);
                return Results.Ok();
            });

            app.MapPut("/movies/watched", async (IMovieRepository repository, int id, bool watched) =>
            {
                var movie = await repository.GetMovieAsync(id);
                if (movie is null)
                    return Results.Problem($"Movie with id {id} was not found", statusCode: 404);

                await repository.UpdateWatchedState(id, watched);
                return Results.Ok();
            });

            app.MapPut("/movies/watchlist", async (IMovieRepository repository, int id, bool isOnWatchlist) =>
            {
                var movie = await repository.GetMovieAsync(id);
                if (movie is null)
                    return Results.Problem($"Movie with id {id} was not found", statusCode: 404);

                await repository.UpdateWatchlistState(id, isOnWatchlist);
                return Results.Ok();
            });

            app.MapDelete("/movies/{id}", async (IMovieRepository repository, int id) =>
            {
                var movie = await repository.GetMovieAsync(id);
                if (movie is null)
                    return Results.Problem($"Movie with id {id} was not found", statusCode: 404);
                await repository.DeleteMovieAsync(id);
                return Results.Ok();
            });
        }
    }
}

