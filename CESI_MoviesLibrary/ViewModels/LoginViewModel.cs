using CESI_MoviesLibrary.Configuration;
using CESI_MoviesLibrary.Data;
using CESI_MoviesLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI_MoviesLibrary.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        //new MovieServiceJson("Data/movies.json")
        private readonly AuthService _authService;
        private readonly NavigationService _navigationService;

        [ObservableProperty] private string email;
        [ObservableProperty] private string password;

        public LoginViewModel(AuthService authService, NavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            var success = await _authService.LoginAsync(Email, Password);
            if (success)
            {
                var user = await _authService.GetUserByEmailAsync(Email);
                if (user != null)
                {
                    var config = AppConfiguration.Load();
                    var apiKey = config.ApiKeys.TheMovieDb;
                    _navigationService.NavigateTo(
                        new DashboardViewModel(
                            new MovieServiceApi(apiKey),
                            new AppDbContext(),
                            user,
                            _authService,
                            _navigationService
                        ));
                }
            }
        }

        [RelayCommand]
        private void GoToRegister()
        {
            _navigationService.NavigateTo(new RegisterViewModel(_authService, _navigationService));
        }
    }
}
