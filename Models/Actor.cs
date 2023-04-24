using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Actor
{
    public int Id { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$")]
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Name { get; set; }
    
    [Display(Name = "Date Of Birth")]
    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }

    public List<Movie>? Movies { get; set; }

}