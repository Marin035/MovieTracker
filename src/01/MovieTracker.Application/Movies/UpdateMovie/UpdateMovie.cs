using MovieTracker.Application.Common.Repositories;
using MovieTracker.Domain.Common.Results;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.UpdateMovie;

public sealed record UpdateMovieRequest(
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl);

public sealed class UpdateMovieHandler(IMovieRepository movieRepository)
{
    public async Task<Result> HandleAsync(
        Guid id,
        UpdateMovieRequest request,
        CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetByIdAsync(id, cancellationToken);
        if (movie is null)
        {
            return Result.Failure(MovieErrors.NotFound);
        }

        if (!Enum.TryParse<MovieGenre>(request.Genre, ignoreCase: true, out var genre))
        {
            return Result.Failure(new Error("Movies.InvalidGenre", $"'{request.Genre}' is not a valid genre."));
        }

        var updateResult = movie.Update(
            request.Title,
            request.Description,
            genre,
            request.ReleaseYear,
            request.PosterUrl);

        if (updateResult.IsFailure)
        {
            return updateResult;
        }

        await movieRepository.UpdateAsync(movie, cancellationToken);
        return Result.Success();
    }
}
