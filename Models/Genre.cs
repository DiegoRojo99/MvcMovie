using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Genre
{
    public Genre(){
        this.Movies = new HashSet<Movie>();
    }

    public Guid Id { get; set; } = Guid.Empty;

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$")]
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Name { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$")]
    [StringLength(120, MinimumLength = 6)]
    public string? Description { get; set; }

    public ICollection<Movie>? Movies { get; set; }

}