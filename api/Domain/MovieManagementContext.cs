using System;
using api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace api.Domain
{
    public class MovieManagementContext : DbContext
    {
        public MovieManagementContext(DbContextOptions<MovieManagementContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Director> Directors => Set<Director>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=movie-management.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Movies)
                .WithOne(m => m.Category);

            modelBuilder.Entity<Director>()
                .HasMany(d => d.Movies)
                .WithOne(m => m.Director);

            Seed.SeedData(modelBuilder);
        }
    }
}

