using IdentityModel.Client;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieApiService(IConfiguration  configuration,IHttpClientFactory httpClientFactory)
        {
            _configuration=configuration?? throw new ArgumentNullException(nameof(configuration));
            _httpClientFactory=httpClientFactory?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies");

            var response = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);


            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var moviesList =  JsonConvert.DeserializeObject<List<Movie>>(content);

            return moviesList;
            
        }

        public Task<IEnumerable<Movie>> GetMovieById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}
