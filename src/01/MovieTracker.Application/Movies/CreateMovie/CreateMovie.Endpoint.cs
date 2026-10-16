using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MovieTracker.Application.Movies.CreateMovie;

public static class CreateMovieEndpoint
{
    public static IEndpointConventionBuilder MapCreateMovieEndpoint(this IEndpointRouteBuilder endpoints)
        => endpoints.MapPost("/api/movies", HandleAsync)
            .WithName("CreateMovie")
            .WithSummary("Creates a new movie")
            .Produces<CreateMovieResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        CreateMovieRequest request,
        IValidator<CreateMovieRequest> validator = null!,
        CreateMovieHandler handler = null!,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var result = await handler.HandleAsync(request, cancellationToken);
        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Created($"/api/movies/{result.Value!.Id}", result.Value);
    }
}
