using CESI_MoviesLibrary.Data;
using CESI_MoviesLibrary.Infrastructure;
using CESI_MoviesLibrary.ViewModels;
using System.Windows;

namespace CESI_MoviesLibrary
{
    public partial class App : Application
    {
        public static MainViewModel MainViewModel { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }

            base.OnStartup(e);

            MainViewModel = new MainViewModel();

            AppServices.Init(MainViewModel);

            MainWindow = new MainWindow
            {
                DataContext = MainViewModel
            };
            MainWindow.Show();

            AppServices.Navigation.NavigateTo(
                new HomeViewModel(AppServices.MovieService, AppServices.Navigation)
            );
        }
    }
}
