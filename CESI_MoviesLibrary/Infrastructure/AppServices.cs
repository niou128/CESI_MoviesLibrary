using CESI_MoviesLibrary.Configuration;
using CESI_MoviesLibrary.Data;
using CESI_MoviesLibrary.Services;

namespace CESI_MoviesLibrary.Infrastructure
{
    public static class AppServices
    {
        private static INavigationHost? _host;
        private static NavigationService? _navigation;
        private static AppDbContext? _db;
        private static AuthService? _auth;
        private static MovieServiceApi? _movieService;

        public static void Init(INavigationHost host)
        {
            _host = host;
            _navigation = new NavigationService(_host);
        }

        public static NavigationService Navigation => _navigation!;
        public static AppDbContext Db => _db ??= new AppDbContext();
        public static AuthService Auth => _auth ??= new AuthService(Db);

        public static MovieServiceApi MovieService
        {
            get
            {
                if (_movieService == null)
                {
                    var config = AppConfiguration.Load();
                    _movieService = new MovieServiceApi(config.ApiKeys.TheMovieDb);
                }
                return _movieService;
            }
        }
    }
}
