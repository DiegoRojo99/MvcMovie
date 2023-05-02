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
                    Id = Guid.Parse("B61659FD-74D4-4670-AA30-98E8F7607288"),
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    RatingId = Guid.Parse("55D72728-18C8-4ABE-BAA9-A60725774C76"),
                    GenreId = Guid.Parse("4ACCB5CD-30D3-448C-A43B-071DE1DBE786")
                },
                new Movie
                {
                    Id = Guid.Parse("1A65BD8E-F74D-444F-85ED-875292332AE5"),
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    RatingId = Guid.Parse("55D72728-18C8-4ABE-BAA9-A60725774C76"),
                    GenreId = Guid.Parse("4ACCB5CD-30D3-448C-A43B-071DE1DBE786"),
                    Poster = "https://i.etsystatic.com/20512669/r/il/b86014/2268047424/il_fullxfull.2268047424_93rq.jpg",
                    Overview = "Three parapsychologists forced out of their university funding set up shop as a unique ghost removal service in New York City, attracting frightened yet skeptical customers."
                },
                new Movie
                {
                    Id = Guid.Parse("664DEA07-3035-4199-8118-7F646FE38BB6"),
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    RatingId = Guid.Parse("A4906C40-0B09-4E00-8E98-28F6030BDF36"),
                    GenreId = Guid.Parse("4ACCB5CD-30D3-448C-A43B-071DE1DBE786")
                },
                new Movie
                {
                    Id = Guid.Parse("A7E22211-263B-42B0-AC90-90421C4224E9"),
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    RatingId = Guid.Parse("7C27B79E-8839-4C56-9489-466D7F43991C"),
                    GenreId = Guid.Parse("3850EB96-5E36-4A26-A3F7-B816E33E2B83")
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
                    Id = Guid.Parse("18375BA7-288E-4F74-9D5E-5C57559650E3"),
                    Name = "Bill Murray",
                    Dob = DateTime.Parse("1950-9-21"),
                    Picture = "https://www.themoviedb.org/t/p/w500/nnCsJc9x3ZiG3AFyiyc3FPehppy.jpg"
                });
            if (context.Star.Any())
            {
                return;   // DB has been seeded
            }
            context.Star.AddRange(
                new Star
                {
                    ActorId = Guid.Parse("18375BA7-288E-4F74-9D5E-5C57559650E3"),
                    MovieId = Guid.Parse("1A65BD8E-F74D-444F-85ED-875292332AE5")
                },new Star
                {
                    ActorId = Guid.Parse("18375BA7-288E-4F74-9D5E-5C57559650E3"),
                    MovieId = Guid.Parse("664DEA07-3035-4199-8118-7F646FE38BB6")
                });
            if (context.StreamingService.Any())
            {
                return;   // DB has been seeded
            }
            context.StreamingService.AddRange(
                new StreamingService
                {
                    Id = Guid.Parse("9C52557C-E17C-4AC0-97F3-267A79B4D718"),
                    Name = "Disney+"
                },new StreamingService
                {
                    Id = Guid.Parse("8993AA19-7FB2-42BB-9C82-623B4E2AF0E6"),
                    Name = "Netflix"
                },new StreamingService
                {
                    Id = Guid.Parse("327C477B-B4C7-4D58-9DE9-BC5FD8CCC88B"),
                    Name = "Amazon Prime"
                });
            if (context.MovieStreaming.Any())
            {
                return;   // DB has been seeded
            }
            context.MovieStreaming.AddRange(
                new MovieStreaming
                {
                    StreamingServiceId = Guid.Parse("9C52557C-E17C-4AC0-97F3-267A79B4D718"),
                    MovieId = Guid.Parse("1A65BD8E-F74D-444F-85ED-875292332AE5")
                },new MovieStreaming
                {
                    StreamingServiceId = Guid.Parse("8993AA19-7FB2-42BB-9C82-623B4E2AF0E6"),
                    MovieId = Guid.Parse("664DEA07-3035-4199-8118-7F646FE38BB6")
                });
            if (context.Genre.Any())
            {
                return;   // DB has been seeded
            }
            context.Genre.AddRange(
                new Genre
                {
                    Id = Guid.Parse("3850EB96-5E36-4A26-A3F7-B816E33E2B83"),
                    Name = "Action",
                    Description = "Involves action like fights"
                },new Genre
                {
                    Id = Guid.Parse("7AFDA635-292F-4291-9CD1-41E6F944A494"),
                    Name = "War",
                    Description = "Story is set in a war conflict"
                },new Genre
                {
                    Id = Guid.Parse("4ACCB5CD-30D3-448C-A43B-071DE1DBE786"),
                    Name = "Comedy",
                    Description = "The movie has the intention of making the viewer laugh"
                },new Genre
                {
                    Name = "Adventure"
                },new Genre
                {
                    Name = "Animation",
                    Description = "The movie is not a live action one"
                },new Genre
                {
                    Name = "Crime",
                    Description = "The movie involves a crime"
                },new Genre
                {
                    Name = "Documentary",
                    Description = "The movie has a dramatic story"
                },new Genre
                {
                    Name = "Drama",
                    Description = "The movie has a docu style"
                },new Genre
                {
                    Name = "Horror",
                    Description = "The movie has the intention of making the viewer fearful"
                },new Genre
                {
                    Name = "Romance",
                    Description = "The movie has a love story as the main plot"
                },new Genre
                {
                    Name = "Western",
                    Description = "The movie is located at the Wild West era"
                });
                 
            if (context.Rating.Any())
            {
                return;   // DB has been seeded
            }
            context.Rating.AddRange(
                new Rating
                {
                    Id = Guid.Parse("8EB6E3A3-4BD5-4005-B57F-BC4151068520"),
                    Name = "G",
                    Description = "General audiences - All ages admitted."
                },new Rating
                {
                    Id = Guid.Parse("A4906C40-0B09-4E00-8E98-28F6030BDF36"),
                    Name = "PG",
                    Description = "Parental guidance suggested - Some material may not be suitable for children."
                },new Rating
                {
                    Id = Guid.Parse("55D72728-18C8-4ABE-BAA9-A60725774C76"),
                    Name = "PG-13",
                    Description = "Parents strongly cautioned - Some material may be inappropriate for children under 13."
                },new Rating
                {
                    Id = Guid.Parse("7C27B79E-8839-4C56-9489-466D7F43991C"),
                    Name = "R",
                    Description = "Restricted - Under 17 requires accompanying parent or adult guardian."
                },new Rating
                {
                    Id = Guid.Parse("1FD50510-9838-49EF-8825-104EF9B702C3"),
                    Name = "NC-17",
                    Description = "Adults Only - No one 17 and under admitted."
                });
                                
            context.SaveChanges();
        }
    }
}