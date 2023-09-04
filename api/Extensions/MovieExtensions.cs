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
        }
    }
}

