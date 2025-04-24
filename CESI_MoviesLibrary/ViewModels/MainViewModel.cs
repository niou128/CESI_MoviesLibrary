using CESI_MoviesLibrary.Data;
using CESI_MoviesLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CESI_MoviesLibrary.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private ObservableObject currentViewModel;

        public NavigationService NavigationService { get; }

        public MainViewModel()
        {
            NavigationService = new NavigationService(this);
            CurrentViewModel = new LoginViewModel(new AuthService(new AppDbContext()), NavigationService);
        }
    }
}
