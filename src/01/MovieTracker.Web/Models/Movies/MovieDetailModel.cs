namespace MovieTracker.Web.Models.Movies;

public sealed record MovieDetailModel(
    Guid Id,
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl);
