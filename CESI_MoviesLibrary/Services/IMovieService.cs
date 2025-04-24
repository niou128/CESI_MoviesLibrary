using CESI_MoviesLibrary.Models;

namespace CESI_MoviesLibrary.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetRandomMoviesAsync();
        Task<IEnumerable<Movie>> SearchMoviesAsync(string query);
    }
}
