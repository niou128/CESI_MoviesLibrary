using CESI_MoviesLibrary.Data;
using CESI_MoviesLibrary.Models;
using CESI_MoviesLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using NavigationService = CESI_MoviesLibrary.Services.NavigationService;

namespace CESI_MoviesLibrary.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IMovieService _movieService;
        private readonly AppDbContext _db;
        private readonly User _currentUser;
        private readonly AuthService _authService;
        private readonly NavigationService _navigationService;

        [ObservableProperty] private string searchQuery;
        [ObservableProperty] private ObservableCollection<string> movieSuggestions = new();
        [ObservableProperty] private ObservableCollection<Movie> searchResults = new();
        [ObservableProperty] private Movie selectedSearchResult;
        [ObservableProperty] private ObservableCollection<Movie> seenMovies = new();
        [ObservableProperty] private ObservableCollection<Movie> wishlistMovies = new();
        [ObservableProperty] private Movie selectedSeenMovie;
        [ObservableProperty] private Movie selectedWishlistMovie;

        public DashboardViewModel(IMovieService movieService, AppDbContext db, User user, AuthService authService, NavigationService navigationService)
        {
            _movieService = movieService;
            _db = db;
            _currentUser = user;
            _authService = authService;
            _navigationService = navigationService;

            LoadUserMovies();
        }

        private void LoadUserMovies()
        {
            var movies = _db.UserMovies.Include(um => um.Movie)
                                       .Where(um => um.UserId == _currentUser.Id)
                                       .ToList();

            SeenMovies = new ObservableCollection<Movie>(movies.Where(m => m.Status == "Seen").Select(m => m.Movie));
            WishlistMovies = new ObservableCollection<Movie>(movies.Where(m => m.Status == "ToWatch").Select(m => m.Movie));
        }

        [RelayCommand]
        private async Task UpdateSuggestionsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;
            var results = await _movieService.SearchMoviesAsync(SearchQuery);
            MovieSuggestions = new ObservableCollection<string>(results.Select(m => m.Title));
            SearchResults = new ObservableCollection<Movie>(results);
        }

        partial void OnSearchQueryChanged(string value)
        {
            UpdateSuggestionsCommand.Execute(null);
        }

        [RelayCommand]
        private void AddSelectedMovieToSeen()
        {
            if (SelectedSearchResult != null)
            {
                SaveUserMovie(SelectedSearchResult, "Seen");
                SeenMovies.Add(SelectedSearchResult);
            }

            ClearSearch();
        }

        [RelayCommand]
        private void AddSelectedMovieToWishlist()
        {
            if (SelectedSearchResult != null)
            {
                SaveUserMovie(SelectedSearchResult, "ToWatch");
                WishlistMovies.Add(SelectedSearchResult);
            }

            ClearSearch();
        }

        [RelayCommand]
        private void RemoveFromSeen()
        {
            if (SelectedSeenMovie != null)
            {
                RemoveUserMovie(SelectedSeenMovie);
                SeenMovies.Remove(SelectedSeenMovie);
            }
        }

        [RelayCommand]
        private void RemoveFromWishlist()
        {
            if (SelectedWishlistMovie != null)
            {
                RemoveUserMovie(SelectedWishlistMovie);
                WishlistMovies.Remove(SelectedWishlistMovie);
            }
        }

        [RelayCommand]
        private void MoveToSeen()
        {
            var movie = SelectedWishlistMovie;
            if (movie != null)
            {
                RemoveUserMovie(movie);
                WishlistMovies.Remove(movie);
                SaveUserMovie(movie, "Seen");
                SeenMovies.Add(movie);
            }
        }

        [RelayCommand]
        private void Logout()
        {
            _navigationService.NavigateTo(new LoginViewModel(_authService, _navigationService));
        }

        private void SaveUserMovie(Movie movie, string status)
        {
            if (movie == null) return;

            var entity = _db.Movies.Find(movie.Id);
            if (entity == null)
            {
                entity = new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Overview = movie.Overview,
                    PosterPath = movie.PosterPath
                };
                _db.Movies.Add(entity);
                _db.SaveChanges();
            }

            var existing = _db.UserMovies.FirstOrDefault(um => um.UserId == _currentUser.Id && um.MovieId == movie.Id);
            if (existing == null)
            {
                var relation = new UserMovie
                {
                    UserId = _currentUser.Id,
                    MovieId = entity.Id,
                    Status = status
                };
                _db.UserMovies.Add(relation);
            }
            else
            {
                existing.Status = status;
                _db.UserMovies.Update(existing);
            }

            _db.SaveChanges();
        }

        private void RemoveUserMovie(Movie movie)
        {
            var entry = _db.UserMovies.FirstOrDefault(x => x.UserId == _currentUser.Id && x.MovieId == movie.Id);
            if (entry != null)
            {
                _db.UserMovies.Remove(entry);
                _db.SaveChanges();
            }
        }

        private void ClearSearch()
        {
            SearchQuery = string.Empty;
            SearchResults.Clear();
            SelectedSearchResult = null;
            MovieSuggestions.Clear();
        }
    }
}
