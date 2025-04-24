using CESI_MoviesLibrary.Models;
using System.IO;
using System.Text.Json;

namespace CESI_MoviesLibrary.Services
{
    public class MovieServiceJson : IMovieService
    {
        private readonly string _filePath;
        private List<Movie>? _cachedMovies;

        public MovieServiceJson(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<List<Movie>> GetRandomMoviesAsync()
        {
            await EnsureMoviesLoaded();
            return _cachedMovies!.OrderBy(_ => Guid.NewGuid()).Take(10).ToList();
        }

        public async Task<List<Movie>> SearchMoviesAsync(string query)
        {
            await EnsureMoviesLoaded();
            return _cachedMovies!
                .Where(m => m.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private async Task EnsureMoviesLoaded()
        {
            if (_cachedMovies != null) return;
            var json = await File.ReadAllTextAsync(_filePath);
            _cachedMovies = JsonSerializer.Deserialize<List<Movie>>(json);
        }
    }
}
