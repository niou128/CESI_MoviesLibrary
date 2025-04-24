using CESI_MoviesLibrary.Models;
using CESI_MoviesLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CESI_MoviesLibrary.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IMovieService _movieService;

        [ObservableProperty]
        private ObservableCollection<Movie> movies = new();

        public HomeViewModel(IMovieService movieService)
        {
            _movieService = movieService;
            LoadMovies();
        }

        private async void LoadMovies()
        {
            var movieList = await _movieService.GetRandomMoviesAsync();
            Movies = new ObservableCollection<Movie>(movieList);
        }
    }
}
