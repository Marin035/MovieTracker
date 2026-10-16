using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.GetMovieById;

internal static class GetMovieByIdMapping
{
    internal static GetMovieByIdResponse ToResponse(this Movie movie) =>
        new(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.Genre.ToString(),
            movie.ReleaseYear,
            movie.PosterUrl);
}
