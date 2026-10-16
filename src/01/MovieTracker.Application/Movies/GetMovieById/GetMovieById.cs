using MovieTracker.Application.Common.Repositories;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.GetMovieById;

public sealed record GetMovieByIdRequest(Guid Id);

public sealed record GetMovieByIdResponse(
    Guid Id,
    string Title,
    string Description,
    string Genre,
    int ReleaseYear,
    string PosterUrl);

public sealed class GetMovieByIdHandler(IMovieRepository movieRepository)
{
    public async Task<GetMovieByIdResponse?> HandleAsync(
        GetMovieByIdRequest request,
        CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetByIdAsync(request.Id, cancellationToken);
        return movie?.ToResponse();
    }
}
