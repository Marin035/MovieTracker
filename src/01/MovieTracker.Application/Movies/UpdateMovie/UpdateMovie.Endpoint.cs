using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.UpdateMovie;

public static class UpdateMovieEndpoint
{
    public static IEndpointConventionBuilder MapUpdateMovieEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapPut("/api/movies/{id:guid}", HandleAsync)
            .WithName("UpdateMovie")
            .WithSummary("Updates an existing movie")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        Guid id,
        UpdateMovieRequest request,
        IValidator<UpdateMovieRequest> validator = null!,
        UpdateMovieHandler handler = null!,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var result = await handler.HandleAsync(id, request, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error == MovieErrors.NotFound
                ? Results.NotFound()
                : Results.BadRequest(result.Error);
        }

        return Results.NoContent();
    }
}
