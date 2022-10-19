using IdentityModel.Client;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient=httpClientFactory.CreateClient("MovieAPIClient")?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClientFactory = httpClientFactory?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContextAccessor = httpContextAccessor?? throw new ArgumentNullException(nameof(_httpContextAccessor));
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

        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var ISClient = _httpClientFactory.CreateClient("ISClient");

            var metaDataResponse = await ISClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the access token");
            }

            var accessToken = await _httpContextAccessor
                .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await ISClient.GetUserInfoAsync(
               new UserInfoRequest
               {
                   Address = metaDataResponse.UserInfoEndpoint,
                   Token = accessToken
               });

            if (userInfoResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while getting user info");
            }

            var userInfoDictionary = new Dictionary<string, string>();

            foreach (var claim in userInfoResponse.Claims)
            {
                userInfoDictionary.Add(claim.Type, claim.Value);
            }

            return new UserInfoViewModel(userInfoDictionary);
        }
    }
}
