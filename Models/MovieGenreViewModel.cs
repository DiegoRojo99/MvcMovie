using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models;

public class MovieGenreViewModel
{
    public List<Movie>? Movies { get; set; }
    public SelectList? Genres { get; set; }
    public Guid? MovieGenre { get; set; }    
    public SelectList? Ratings { get; set; }
    public Guid? MovieStream { get; set; }  
    public SelectList? Streamings { get; set; }
    public string? MovieRating { get; set; }
    public SelectList? Directors { get; set; }
    public string? DirectorString { get; set; }
    public string? SearchString { get; set; }
    public int? Page { get; set; }
    public int? TotalPages {get;set;}
}