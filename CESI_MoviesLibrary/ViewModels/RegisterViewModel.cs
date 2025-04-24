using CESI_MoviesLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CESI_MoviesLibrary.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly NavigationService _navigationService;

        [ObservableProperty] private string name;
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;

        public RegisterViewModel(AuthService authService, NavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            var success = await _authService.RegisterAsync(Name, Email, Password);
            if (success)
            {
                _navigationService.NavigateTo(new LoginViewModel(_authService, _navigationService));
            }
        }

        [RelayCommand]
        private void GoToLogin()
        {
            _navigationService.NavigateTo(new LoginViewModel(_authService, _navigationService));
        }
    }
}
