using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }
            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Price = 7.99M,
                    Rating ="R"
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Price = 8.99M,
                    Rating ="PG"
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Price = 9.99M,
                    Rating ="PG"
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Price = 3.99M,
                    Rating ="R"
                }
            );
            //Look for actors
            if (context.Actor.Any())
            {
                return;   // DB has been seeded
            }
            context.Actor.AddRange(
                new Actor
                {
                    Name = "Bill Murray",
                    Dob = DateTime.Parse("1950-9-21")
                });

            if (context.Genre.Any())
            {
                return;   // DB has been seeded
            }
            context.Genre.AddRange(
                new Genre
                {
                    Name = "Action",
                    Description = "Involves action like fights"
                },new Genre
                {
                    Name = "War",
                    Description = "Story is set in a war conflict"
                },new Genre
                {
                    Name = "Comedy",
                    Description = "The movie has the intention of making the viewer laugh"
                });
                 
                                
            context.SaveChanges();
        }
    }
}