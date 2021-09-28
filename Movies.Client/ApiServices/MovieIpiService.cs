using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Client.Models;

namespace Movies.Client.ApiServices
{
    public class MovieIpiService : IMovieIpiService
    {
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movieList = new List<Movie>
            {
                new Movie()
                {
                    Id = 1,
                    Genre = "Comics",
                    ImageUrl = "image/src",
                    Owner = "Dima",
                    Rating ="5,5" ,
                    ReleaseDate = DateTime.Now ,
                    Title = "asd"
                },
                new Movie()
                {
                    Id = 2,
                    Genre = "Triller",
                    ImageUrl = "image/src",
                    Owner = "Vova",
                    Rating ="5,5" ,
                    ReleaseDate = DateTime.Now ,
                    Title = "asd"
                }
            };

            return await Task.FromResult(movieList);
        }

        public Task<Movie> GetMovie()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> CreateMovies(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateMovies(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
