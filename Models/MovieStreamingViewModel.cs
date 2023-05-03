using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models;

public class MovieStreamingViewModel
{
    public SelectList? Movies { get; set; }
    public List<MovieStreaming>? Streams { get; set; }
    public SelectList? CountryCodes { get; set; }
    public Guid? CountryCode { get; set; }    
    public Guid? Movie { get; set; }    
}