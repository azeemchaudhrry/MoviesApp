using MovieApp.Entities;
using System;
using System.Threading.Tasks;

namespace MovieApp.Contracts
{
    public interface IMovieService
    {
        Task<ResponseObj<Movie>> DiscoverMovies(MovieRequest request);
        Task<ResponseObj<Movie>> SearchMovies(MovieRequest request);
        Task<Genres> GetGenres();
    }
}
