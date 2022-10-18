using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var movies = new List<Movie>
                {
                    new Movie {
                        Id = 1,
                        Genre = "Drama",
                        Title = "The shawshank Redemption",
                        Rating ="5.3",
                        ImageUrl ="images/src",
                        ReleaseTime=new DateTime(1994,5,5),
                        Owner ="alice"
                    }
            };

            return await Task.FromResult(movies);
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
