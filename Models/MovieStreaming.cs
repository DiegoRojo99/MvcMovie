using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class MovieStreaming
{
    [Key]
    public int MovieStreamingId { get; set; }
    public Guid? MovieId { get; set; }
    public Guid? StreamingServiceId { get; set; }
    public Movie? Movie { get; set; }
    public StreamingService? StreamingService { get; set; }
    public String? CountryCode { get; set; }

}