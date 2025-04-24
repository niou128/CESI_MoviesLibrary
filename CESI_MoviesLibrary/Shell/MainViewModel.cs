using CESI_MoviesLibrary.Data;
using CESI_MoviesLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CESI_MoviesLibrary.ViewModels
{
    public partial class MainViewModel : ObservableObject, INavigationHost
    {
        [ObservableProperty] private ObservableObject currentViewModel;

        public NavigationService NavigationService { get; }

        public MainViewModel()
        {
        }
    }
}
