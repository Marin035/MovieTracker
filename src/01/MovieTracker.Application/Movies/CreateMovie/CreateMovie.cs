using MovieTracker.Application.Common.Repositories;
using MovieTracker.Domain.Common.Results;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.CreateMovie;

public sealed record CreateMovieRequest(
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl);

public sealed record CreateMovieResponse(Guid Id);

public sealed class CreateMovieHandler(IMovieRepository movieRepository)
{
    public async Task<Result<CreateMovieResponse>> HandleAsync(
        CreateMovieRequest request,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<MovieGenre>(request.Genre, ignoreCase: true, out var genre))
        {
            return new Error("Movies.InvalidGenre", $"'{request.Genre}' is not a valid genre.");
        }

        var result = Movie.Create(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            genre,
            request.ReleaseYear,
            request.PosterUrl);

        if (result.IsFailure)
        {
            return Result<CreateMovieResponse>.Failure(result.Error);
        }

        await movieRepository.AddAsync(result.Value!, cancellationToken);
        return Result<CreateMovieResponse>.Success(result.Value!.ToResponse());
    }
}
