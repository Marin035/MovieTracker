using MovieTracker.Application.Common.Repositories;
using MovieTracker.Domain.Common.Results;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.DeleteMovie;

public sealed record DeleteMovieRequest(Guid Id);

public sealed class DeleteMovieHandler(IMovieRepository movieRepository)
{
    public async Task<Result> HandleAsync(
        DeleteMovieRequest request,
        CancellationToken cancellationToken)
    {
        var deleted = await movieRepository.DeleteAsync(request.Id, cancellationToken);
        return deleted ? Result.Success() : Result.Failure(MovieErrors.NotFound);
    }
}
