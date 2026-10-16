using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.CreateMovie;

internal static class CreateMovieMapping
{
    internal static CreateMovieResponse ToResponse(this Movie movie) =>
        new(movie.Id);
}
