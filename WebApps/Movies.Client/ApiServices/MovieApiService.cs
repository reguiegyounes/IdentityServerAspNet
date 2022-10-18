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

        public MovieApiService(IConfiguration  configuration)
        {
            _configuration=configuration;
        }
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            // "retrieve" our api credetials . This must be registred on Identity Server
            var apiClientCredentials = new ClientCredentialsTokenRequest
            {
                Address=$"{_configuration["oidc:authority_url"]}/connect/token",
                ClientId=_configuration["Movies.API:ClientId"],
                ClientSecret=_configuration["Movies.API:ClientSecret"],
                Scope= _configuration["Movies.API:Scope"]
            };
            
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(_configuration["oidc:authority_url"]);
            if (disco.IsError)
            {
                return null; // throw 500 error
            }
            // get an access token 
            var tokenResponse=await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
            if (tokenResponse.IsError)
            {
                return null;
            }


            // Send request to protected api

            var apiClient=new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync($"{_configuration["Movies.API:Url"]}/api/movies");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            List<Movie> moviesList =  JsonConvert.DeserializeObject<List<Movie>>(content);

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
