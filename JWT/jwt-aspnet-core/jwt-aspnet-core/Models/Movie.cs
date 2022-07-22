using System.ComponentModel.DataAnnotations;

namespace jwt_aspnet_core.Models;

public class Movie 
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Genre is required")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "Director is required")]
    public string Director { get; set; }
}