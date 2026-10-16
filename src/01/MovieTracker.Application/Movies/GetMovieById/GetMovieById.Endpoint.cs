using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MovieTracker.Application.Movies.GetMovieById;

public static class GetMovieByIdEndpoint
{
    public static IEndpointConventionBuilder MapGetMovieByIdEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/api/movies/{id:guid}", HandleAsync)
            .WithName("GetMovieById")
            .WithSummary("Gets a movie by its id")
            .Produces<GetMovieByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

    private static async Task<IResult> HandleAsync(
        Guid id,
        GetMovieByIdHandler handler = null!,
        CancellationToken cancellationToken = default)
    {
        var response = await handler.HandleAsync(new GetMovieByIdRequest(id), cancellationToken);
        return response is not null
            ? Results.Ok(response)
            : Results.NotFound();
    }
}
