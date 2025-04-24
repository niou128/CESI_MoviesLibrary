using CommunityToolkit.Mvvm.ComponentModel;

namespace CESI_MoviesLibrary.Services
{
    public interface INavigationHost
    {
        ObservableObject CurrentViewModel { get; set; }
    }
}
