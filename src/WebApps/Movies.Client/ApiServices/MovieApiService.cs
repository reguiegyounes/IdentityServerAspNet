using IdentityModel.Client;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MovieApiService(IConfiguration  configuration,IHttpClientFactory httpClientFactory)
        {
            _configuration=configuration?? throw new ArgumentNullException(nameof(configuration));
            _httpClient=httpClientFactory.CreateClient("MovieAPIClient")?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies");

            var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);


            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var moviesList =  JsonConvert.DeserializeObject<List<Movie>>(content);

            return moviesList;
            
        }

        public async Task<Movie> GetMovieById(int? id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/movies/{id}");

            var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);


            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<Movie>(content);

            return movie;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            if (movie == null) throw new Exception("The body of request must be not empty"); 

            var response = await _httpClient.PostAsJsonAsync("/api/movies", movie);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var newMovie = JsonConvert.DeserializeObject<Movie>(content);

            return newMovie;
        }

        public async Task DeleteMovie(int id)
        {
            await _httpClient.DeleteAsync($"/api/movies/{id}");
        }

        public async Task<Movie> UpdateMovie(int? id,Movie movie)
        {
            if (movie == null) throw new Exception("The body of request must be not empty");

            var response = await _httpClient.PutAsJsonAsync($"/api/movies/{id}", movie);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var editedMovie = JsonConvert.DeserializeObject<Movie>(content);

            return editedMovie;
        }
    }
}
