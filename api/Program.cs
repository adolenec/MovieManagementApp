using api.Domain;
using api.Extensions;
using api.Interfaces;
using api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<MovieManagementContext>(options =>
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:4200")
    .AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.MapMovieEndpoints();
app.MapCategoryEndpoints();

app.UseHttpsRedirection();

app.Run();
