using MoviesApp.Model;

namespace MoviesApp.Services
{
    public interface IGeneresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> AddGenre(Genre genre); 
        Genre Update(Genre genre); 
        Genre Delete(Genre genre);
        Task<Genre> GetById(byte id);
        Task<bool> IsValidGenre(byte id);

    }
}
