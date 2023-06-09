using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Star
{
    [Key]
    public int StarId { get; set; }
    public Guid? MovieId { get; set; }
    public Guid? ActorId { get; set; }
    public Movie? Movie { get; set; }
    public Actor? Actor { get; set; }

}