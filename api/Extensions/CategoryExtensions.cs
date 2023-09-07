using System;
using api.Dtos;
using api.Interfaces;

namespace api.Extensions
{
	public static class CategoryExtensions
	{
        public static void MapCategoryEndpoints(this WebApplication app)
        {
            app.MapGet("/categories", (ICategoryRepository repository, int page, int pageSize, string? searchTerm) =>
                repository.GetCategoriesAsync(page, pageSize, searchTerm));

            app.MapGet("/categories/dropdown", (ICategoryRepository repository, string? searchTerm) =>
                repository.GetDropdownCategoriesAsync(searchTerm));

            app.MapGet("categories/{id}", async (ICategoryRepository repository, int id) =>
            {
                var category = await repository.GetCategoryAsync(id);

                if (category is null)
                    return Results.Problem($"Movie with Id {id} was not found", statusCode: 404);
                return Results.Ok(category);
            });

            app.MapPost("/categories", async (ICategoryRepository repository, CreateCategoryDto createCategoryDto) =>
            {
                var category = await repository.AddCategoryAsync(createCategoryDto);
                return Results.Created($"/categories/{category.Id}", category);
            });

            app.MapPut("/categories/{id}", async (UpdateCategoryDto categoryDto, int id, ICategoryRepository repository) =>
            {
                var category = await repository.GetCategoryAsync(id);
                if (category is null)
                    return Results.Problem($"Category with id {id} was not found", statusCode: 404);

                var updatedCategory = await repository.UpdateCategoryAsync(categoryDto, id);
                return Results.Ok(updatedCategory);
            });

            app.MapDelete("/categories/{id}", async (ICategoryRepository repository, int id) =>
            {
                var category = await repository.GetCategoryAsync(id);
                if (category is null)
                    return Results.Problem($"Category with id {id} was not found", statusCode: 404);
                await repository.DeleteCategoryAsync(id);
                return Results.Ok();
            });
        }
    }
}

