using CESI_MoviesLibrary.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CESI_MoviesLibrary.Services
{
    public class NavigationService
    {
        private readonly MainViewModel _mainViewModel;

        public NavigationService(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void NavigateTo(ObservableObject viewModel)
        {
            _mainViewModel.CurrentViewModel = viewModel;
        }
    }
}
