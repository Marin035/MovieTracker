using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Web.Models.Movies;

public sealed class UpdateMovieModel
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Genre is required")]
    public string Genre { get; set; } = "ScienceFiction";

    [Required(ErrorMessage = "Release year is required")]
    [Range(1888, 2100, ErrorMessage = "Release year must be 1888 or later")]
    public int ReleaseYear { get; set; } = DateTime.UtcNow.Year;

    [Required(ErrorMessage = "Poster URL is required")]
    public string PosterUrl { get; set; } = string.Empty;
}
