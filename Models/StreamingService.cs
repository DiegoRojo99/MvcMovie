using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class StreamingService
{

    public Guid Id { get; set; } = Guid.Empty;

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Name { get; set; }

    public string? LogoImage { get; set; }

    public ICollection<MovieStreaming>? Movies { get; set; }
}