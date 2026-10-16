using FluentValidation;
using MovieTracker.Domain.Movies;

namespace MovieTracker.Application.Movies.UpdateMovie;

public sealed class UpdateMovieValidator : AbstractValidator<UpdateMovieRequest>
{
    public UpdateMovieValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("A movie must have a title.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A movie must have a description.");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("A movie must have a genre.")
            .IsEnumName(typeof(MovieGenre), caseSensitive: false).WithMessage("'{PropertyValue}' is not a valid genre.");

        RuleFor(x => x.ReleaseYear)
            .GreaterThanOrEqualTo(1888).WithMessage("A movie's release year cannot be earlier than 1888.");

        RuleFor(x => x.PosterUrl)
            .NotEmpty().WithMessage("A movie must have a poster URL.");
    }
}
