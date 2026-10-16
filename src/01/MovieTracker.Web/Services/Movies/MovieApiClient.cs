using System.Net;
using System.Net.Http.Json;
using MovieTracker.Web.Models.Movies;

namespace MovieTracker.Web.Services.Movies;

public sealed class MovieApiClient(HttpClient httpClient)
{
    public async Task<MovieListResponseModel> GetMoviesAsync(int page = 1, int pageSize = 24, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"api/movies?page={page}&pageSize={pageSize}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MovieListResponseModel>(cancellationToken)
            ?? throw new InvalidOperationException("The movie API returned an empty response.");
    }

    public async Task<MovieDetailModel?> GetMovieByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"api/movies/{id}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MovieDetailModel>(cancellationToken);
    }

    public async Task<Guid> CreateMovieAsync(CreateMovieModel model, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/movies", model, cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<CreateMovieResponseModel>(cancellationToken);
        return result?.Id ?? throw new InvalidOperationException("The movie API did not return an ID.");
    }

    public async Task UpdateMovieAsync(Guid id, UpdateMovieModel model, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"api/movies/{id}", model, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteMovieAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"api/movies/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
