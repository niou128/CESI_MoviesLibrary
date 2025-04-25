using CESI_MoviesLibrary.Dtos;
using CESI_MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CESI_MoviesLibrary.Services
{
   public class MovieServiceApi : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.themoviedb.org/3";

        public MovieServiceApi(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IEnumerable<Movie>> GetRandomMoviesAsync()
        {
            // Films populaires comme exemple
            var url = $"{BaseUrl}/movie/popular?api_key={_apiKey}&language=fr-FR&page=1";
            var response = await _httpClient.GetFromJsonAsync<TmdbMovieResponse>(url);

            return response?.results.Select(MapToMovie) ?? new List<Movie>();
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return Enumerable.Empty<Movie>();

            var url = $"{BaseUrl}/search/movie?api_key={_apiKey}&language=fr-FR&query={Uri.EscapeDataString(query)}&page=1";
            var response = await _httpClient.GetFromJsonAsync<TmdbMovieResponse>(url);

            return response?.results.Select(MapToMovie) ?? Enumerable.Empty<Movie>();
        }

        private Movie MapToMovie(TmdbMovieDto dto)
        {
            return new Movie
            {
                Id = dto.id,
                Title = dto.title,
                Overview = dto.overview,
                PosterPath = $"https://image.tmdb.org/t/p/w500{dto.poster_path}"
            };
        }
    }
}
