using Microsoft.Extensions.Configuration;

namespace CESI_MoviesLibrary.Configuration
{
    public class AppConfiguration
    {
        public ApiKeysConfig ApiKeys { get; set; } = new();

        public class ApiKeysConfig
        {
            public string TheMovieDb { get; set; } = string.Empty;
        }

        public static AppConfiguration Load()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var config = new AppConfiguration();
            builder.Build().Bind(config);
            return config;
        }

    }
}
