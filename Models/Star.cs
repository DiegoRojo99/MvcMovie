using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Star
{
    [Key]
    public int StarId { get; set; }
    public int MovieId { get; set; }
    public int ActorId { get; set; }

}