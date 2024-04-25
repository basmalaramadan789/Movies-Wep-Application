using MoviesApp.Model;

namespace MoviesApp.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(byte GenreId=0);
        Task<Movie> GetById(int id);
        Task<Movie> AddMovie(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
        
    }
}
