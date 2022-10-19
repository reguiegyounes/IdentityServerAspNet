using Movies.Client.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.ApiServices
{
    public interface IMovieApiService
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(int? id);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(int? id,Movie movie);
        Task DeleteMovie(int id);
        Task<UserInfoViewModel> GetUserInfo();
    }
}
