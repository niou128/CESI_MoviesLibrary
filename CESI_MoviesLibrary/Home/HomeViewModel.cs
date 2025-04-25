using CESI_MoviesLibrary.Data;
using CESI_MoviesLibrary.Models;
using CESI_MoviesLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CESI_MoviesLibrary.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IMovieService _movieService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Movie> randomMovies = new();

        public HomeViewModel(IMovieService movieService, NavigationService navigationService)
        {
            _movieService = movieService;
            _navigationService = navigationService;

            LoadMovies();
        }

        private async void LoadMovies()
        {
            var movies = await _movieService.GetRandomMoviesAsync();
            RandomMovies = new ObservableCollection<Movie>(movies);
        }

        [RelayCommand]
        private void GoToLogin()
        {
            var authService = new AuthService(new AppDbContext());
            _navigationService.NavigateTo(new LoginViewModel(authService, _navigationService));
        }
    }
}
