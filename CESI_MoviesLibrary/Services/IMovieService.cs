using CESI_MoviesLibrary.Models;

namespace CESI_MoviesLibrary.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetRandomMoviesAsync();
        Task<List<Movie>> SearchMoviesAsync(string query);
    }
}
