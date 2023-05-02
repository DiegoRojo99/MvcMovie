using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Movie
{

    public Guid Id { get; set; } = Guid.Empty;

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Title { get; set; }
    
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    public Rating? Rating { get; set; }
    public Guid? RatingId { get; set; }
    
    public ICollection<Star>? Stars { get; set; }
    public ICollection<MovieStreaming>? Streams { get; set; }
    public Guid? GenreId { get; set; }
    public Genre? Genre { get; set; }
}