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
            
            if (context.Movie.Any()){
                return;   // DB has been seeded
            }
            else{   
            context.Movie.AddRange(
                new Movie
                {
                    Id = Guid.Parse("B61659FD-74D4-4670-AA30-98E8F7607288"),
                    Title = "The Avengers",
                    ReleaseDate = DateTime.Parse("2012-2-12"),
                    RatingId = Guid.Parse("55D72728-18C8-4ABE-BAA9-A60725774C76"),
                    GenreId = Guid.Parse("3850EB96-5E36-4A26-A3F7-B816E33E2B83"),
                    Poster = "https://www.themoviedb.org/t/p/w500/RYMX2wcKCBAr24UyPD7xwmjaTn.jpg",
                    DirectorId = Guid.Parse("338C2170-DC31-4CEF-9C7D-F0EE33F60392"),
                    Overview ="Earth's mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity."

                },
                new Movie{
                    Id = Guid.Parse("F879043B-EBC7-4368-9DE6-5C9148901C7C"),
                    Title = "The Super Mario Bros. Movie",
                    ReleaseDate = DateTime.Parse("2023-4-7"),
                    RatingId = Guid.Parse("8EB6E3A3-4BD5-4005-B57F-BC4151068520"),
                    GenreId = Guid.Parse("500E34D8-9118-4B03-ABE6-AE89A7A3C194"),
                    Poster= "https://www.themoviedb.org/t/p/w500/qNBAXBIQlnOThrVvA6mA2B5ggV6.jpg",
                    Overview = "The story of The Super Mario Bros. on their journey through the Mushroom Kingdom."
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
                    GenreId = Guid.Parse("4ACCB5CD-30D3-448C-A43B-071DE1DBE786"),
                    Poster = "https://m.media-amazon.com/images/M/MV5BMTQ2NTk4MjE5Ml5BMl5BanBnXkFtZTgwODIwNjYxMTE@._V1_FMjpg_UX1000_.jpg",
                    Overview = "The discovery of a massive river of ectoplasm and a resurgence of spectral activity allows the staff of Ghostbusters to revive the business."
                },
                new Movie
                {
                    Id = Guid.Parse("A7E22211-263B-42B0-AC90-90421C4224E9"),
                    Title = "Django Unchained",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    RatingId = Guid.Parse("7C27B79E-8839-4C56-9489-466D7F43991C"),
                    GenreId = Guid.Parse("3850EB96-5E36-4A26-A3F7-B816E33E2B83"),
                    Poster = "https://www.themoviedb.org/t/p/w500/7oWY8VDWW7thTzWh3OKYRkWUlD5.jpg",
                    Overview = "With the help of a German bounty-hunter, a freed slave sets out to rescue his wife from a brutal plantation owner in Mississippi.",
                    DirectorId = Guid.Parse("B11CDF3D-6A6F-4EB7-A322-046E4379FEF4")
                },
                new Movie
                {
                    Id = Guid.Parse("18967EB4-E653-4852-8D18-016f489E76AB"),
                    Title = "Fight Club",
                    ReleaseDate = DateTime.Parse("2001-4-15"),
                    RatingId = Guid.Parse("7C27B79E-8839-4C56-9489-466D7F43991C"),
                    GenreId = Guid.Parse("3850EB96-5E36-4A26-A3F7-B816E33E2B83"),
                    Poster = "https://www.themoviedb.org/t/p/w150_and_h225_bestv2/pB8BM7pdSp6B6Ih7QZ4DrQ3PmJK.jpg",
                    Overview = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more."
                }
            );
            }
            
            if (context.Actor.Any()){
                return;   // DB has been seeded
            }
            else{
            context.Actor.AddRange(
                new Actor
                {
                    Id = Guid.Parse("18375BA7-288E-4F74-9D5E-5C57559650E3"),
                    Name = "Bill Murray",
                    Dob = DateTime.Parse("1950-9-21"),
                    Picture = "https://www.themoviedb.org/t/p/w500/nnCsJc9x3ZiG3AFyiyc3FPehppy.jpg"
                },
                new Actor{
                    Id = Guid.Parse("DFC7F83B-B60C-4092-B166-A300BD9AD2B1"),
                    Name = "Brad Pitt",
                    Dob = DateTime.Parse("1950-9-21"),
                    Picture = "https://www.themoviedb.org/t/p/original/tJiSUYst4ddIaz1zge2LqCtu9tw.jpg"
                },
                new Actor{
                    Id = Guid.Parse("10067C7B-B8AA-4625-821B-481233851368"),
                    Name = "Keanu Reeves",
                    Dob = DateTime.Parse("1950-9-21"),
                    Picture = "https://www.themoviedb.org/t/p/w500/4D0PpNI0kmP58hgrwGC3wCjxhnm.jpg"
                },
                new Actor{
                    Id = Guid.Parse("24CA6D2B-28B8-48F9-A4FF-2AFB658668F4"),
                    Name = "Edward Norton",
                    Dob = DateTime.Parse("1961-9-21"),
                    Picture = "https://www.themoviedb.org/t/p/w500/8nytsqL59SFJTVYVrN72k6qkGgJ.jpg"
                },
                new Actor{
                    Id = Guid.Parse("7E8AEE99-ADC9-4614-90CC-5ACB4CA754D6"),
                    Name = "Chris Pratt",
                    Dob = DateTime.Parse("1982-8-21"),
                    Picture = "https://www.themoviedb.org/t/p/original/83o3koL82jt30EJ0rz4Bnzrt2dd.jpg"
                },
                new Actor{
                    Id = Guid.Parse("6B04261C-C74A-4FD4-9576-39930AF383B6"),
                    Name = "Christoph Waltz",
                    Dob = DateTime.Parse("1956-8-4"),
                    Picture = "https://www.themoviedb.org/t/p/w500/2Hhztd4mUEV9Y25rfkXDwzL9QI9.jpg"
                },
                new Actor{
                    Id = Guid.Parse("D329B031-D47A-46DD-BA66-2C7DE694735C"),
                    Name = "Jamie Foxx",
                    Dob = DateTime.Parse("1967-12-13"),
                    Picture = "https://www.themoviedb.org/t/p/original/oZa2WXbsXRfJp7eI2oorREAaDrd.jpg"
                },
                new Actor{
                    Id = Guid.Parse("AF077D9B-D47A-43F9-9E33-67F561F52482"),
                    Name = "Leonardo Di Caprio",
                    Dob = DateTime.Parse("1974-11-11"),
                    Picture = "https://upload.wikimedia.org/wikipedia/commons/4/46/Leonardo_Dicaprio_Cannes_2019.jpg"
                });
            
            }
            
            if (context.Star.Any()){
                return;   // DB has been seeded
            }
            else{
            context.Star.AddRange(
                new Star
                {
                    ActorId = Guid.Parse("18375BA7-288E-4F74-9D5E-5C57559650E3"),
                    MovieId = Guid.Parse("1A65BD8E-F74D-444F-85ED-875292332AE5")
                },
                new Star
                {
                    ActorId = Guid.Parse("18375BA7-288E-4F74-9D5E-5C57559650E3"),
                    MovieId = Guid.Parse("664DEA07-3035-4199-8118-7F646FE38BB6")
                },
                new Star
                {
                    ActorId = Guid.Parse("24CA6D2B-28B8-48F9-A4FF-2AFB658668F4"),
                    MovieId = Guid.Parse("18967EB4-E653-4852-8D18-016f489E76AB")
                },
                new Star
                {
                    ActorId = Guid.Parse("DFC7F83B-B60C-4092-B166-A300BD9AD2B1"),
                    MovieId = Guid.Parse("18967EB4-E653-4852-8D18-016f489E76AB")
                },
                new Star{
                    MovieId = Guid.Parse("F879043B-EBC7-4368-9DE6-5C9148901C7C"),
                    ActorId = Guid.Parse("7E8AEE99-ADC9-4614-90CC-5ACB4CA754D6")
                },
                new Star{
                    MovieId = Guid.Parse("A7E22211-263B-42B0-AC90-90421C4224E9"),
                    ActorId = Guid.Parse("D329B031-D47A-46DD-BA66-2C7DE694735C")
                },
                new Star{
                    MovieId = Guid.Parse("A7E22211-263B-42B0-AC90-90421C4224E9"),
                    ActorId = Guid.Parse("6B04261C-C74A-4FD4-9576-39930AF383B6")
                },
                new Star{
                    MovieId = Guid.Parse("A7E22211-263B-42B0-AC90-90421C4224E9"),
                    ActorId = Guid.Parse("AF077D9B-D47A-43F9-9E33-67F561F52482")
                });
            
            }
           
            if (context.StreamingService.Any()){
                return;   // DB has been seeded
            }
            else{
            context.StreamingService.AddRange(
                new StreamingService
                {
                    Id = Guid.Parse("9C52557C-E17C-4AC0-97F3-267A79B4D718"),
                    Name = "Disney+",
                    LogoImage = "https://store-images.s-microsoft.com/image/apps.6899.14495311847124170.e89a4dce-fd9a-4a10-b8e4-a6c3aa1c055e.bcea8b9e-9a72-45eb-a2fc-e265b7dc1915"
                },
                new StreamingService
                {
                    Id = Guid.Parse("8993AA19-7FB2-42BB-9C82-623B4E2AF0E6"),
                    Name = "Netflix",
                    LogoImage = "https://play-lh.googleusercontent.com/TBRwjS_qfJCSj1m7zZB93FnpJM5fSpMA_wUlFDLxWAb45T9RmwBvQd5cWR5viJJOhkI"
                },
                new StreamingService
                {
                    Id = Guid.Parse("327C477B-B4C7-4D58-9DE9-BC5FD8CCC88B"),
                    Name = "Amazon Prime",
                    LogoImage = "https://play-lh.googleusercontent.com/VojafVZNddI6JvdDGWFrRmxc-prrcInL2AuBymsqGoeXjT4f9sv7KnetB-v3iLxk_Koi"
                });
            
            }
            
            if (context.MovieStreaming.Any()){
                return;   // DB has been seeded
            }
            else{
            context.MovieStreaming.AddRange(
                new MovieStreaming
                {
                    StreamingServiceId = Guid.Parse("9C52557C-E17C-4AC0-97F3-267A79B4D718"),
                    MovieId = Guid.Parse("1A65BD8E-F74D-444F-85ED-875292332AE5"),
                    CountryCode = "US"
                },
                new MovieStreaming
                {
                    StreamingServiceId = Guid.Parse("8993AA19-7FB2-42BB-9C82-623B4E2AF0E6"),
                    MovieId = Guid.Parse("664DEA07-3035-4199-8118-7F646FE38BB6"),
                    CountryCode = "ES"
                });
            
            }
            if (context.Rating.Any()){
                return;   // DB has been seeded
            }
            else{
            context.Rating.AddRange(
                new Rating
                {
                    Id = Guid.Parse("8EB6E3A3-4BD5-4005-B57F-BC4151068520"),
                    Name = "G",
                    Description = "General audiences - All ages admitted."
                },
                new Rating
                {
                    Id = Guid.Parse("A4906C40-0B09-4E00-8E98-28F6030BDF36"),
                    Name = "PG",
                    Description = "Parental guidance suggested - Some material may not be suitable for children."
                },
                new Rating
                {
                    Id = Guid.Parse("55D72728-18C8-4ABE-BAA9-A60725774C76"),
                    Name = "PG-13",
                    Description = "Parents strongly cautioned - Some material may be inappropriate for children under 13."
                },
                new Rating
                {
                    Id = Guid.Parse("7C27B79E-8839-4C56-9489-466D7F43991C"),
                    Name = "R",
                    Description = "Restricted - Under 17 requires accompanying parent or adult guardian."
                },
                new Rating
                {
                    Id = Guid.Parse("1FD50510-9838-49EF-8825-104EF9B702C3"),
                    Name = "NC-17",
                    Description = "Adults Only - No one 17 and under admitted."
                });
            }
            
            if (context.Genre.Any()){
                return;   // DB has been seeded
            }
            else{
              
            context.Genre.AddRange(
                new Genre
                {
                    Id = Guid.Parse("3850EB96-5E36-4A26-A3F7-B816E33E2B83"),
                    Name = "Action",
                    Description = "Involves action like fights"
                },
                new Genre
                {
                    Id = Guid.Parse("7AFDA635-292F-4291-9CD1-41E6F944A494"),
                    Name = "War",
                    Description = "Story is set in a war conflict"
                },
                new Genre
                {
                    Id = Guid.Parse("4ACCB5CD-30D3-448C-A43B-071DE1DBE786"),
                    Name = "Comedy",
                    Description = "The movie has the intention of making the viewer laugh"
                },
                new Genre
                {
                    Name = "Adventure"
                },
                new Genre
                {
                    Id = Guid.Parse("500E34D8-9118-4B03-ABE6-AE89A7A3C194"),
                    Name = "Animation",
                    Description = "The movie is not a live action one"
                },
                new Genre
                {
                    Name = "Crime",
                    Description = "The movie involves a crime"
                },
                new Genre
                {
                    Name = "Documentary",
                    Description = "The movie has a dramatic story"
                },
                new Genre
                {
                    Name = "Drama",
                    Description = "The movie has a docu style"
                },
                new Genre
                {
                    Name = "Horror",
                    Description = "The movie has the intention of making the viewer fearful"
                },
                new Genre
                {
                    Name = "Romance",
                    Description = "The movie has a love story as the main plot"
                },
                new Genre
                {
                    Name = "Western",
                    Description = "The movie is located at the Wild West era"
                });
                   
            }

            if(context.Director.Any()){
                return;
            }
            else{
                context.Director.AddRange(
                    new Director{
                        Id = Guid.Parse("B11CDF3D-6A6F-4EB7-A322-046E4379FEF4"),
                        Name = "Quentin Tarantino",
                        Dob = DateTime.Parse("1963-3-27"),
                        Picture = "https://www.themoviedb.org/t/p/w500/1gjcpAa99FAOWGnrUvHEXXsRs7o.jpg"
                    },
                    new Director{
                        Id = Guid.Parse("338C2170-DC31-4CEF-9C7D-F0EE33F60392"),
                        Name = "Josh Whedon",
                        Dob = DateTime.Parse("1964-6-23"),
                        Picture = "https://www.themoviedb.org/t/p/original/sZ3RzktbrooBpRHkoUCvQQrmgWu.jpg"
                    }
                );
                
            }
            
            
                                
            context.SaveChanges();
        }
    }
}