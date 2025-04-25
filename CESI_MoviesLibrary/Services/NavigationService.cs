using CommunityToolkit.Mvvm.ComponentModel;

namespace CESI_MoviesLibrary.Services
{
    public class NavigationService
    {
        private readonly INavigationHost _host;

        public NavigationService(INavigationHost host)
        {
            _host = host;
        }

        public void NavigateTo(ObservableObject viewModel)
        {
            _host.CurrentViewModel = viewModel;
        }
    }
}
