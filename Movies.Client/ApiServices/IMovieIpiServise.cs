using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Client.Models;

namespace Movies.Client.ApiServices
{
    public interface IMovieIpiService
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie> GetMovie();
        Task<Movie> CreateMovies(Movie movie);
        Task<Movie> UpdateMovies(Movie movie);
        Task Delete(int id);
        string GetUserInfo();
    }
}
