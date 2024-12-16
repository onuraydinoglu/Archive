using ArchiveApp.Models;

namespace ArchiveApp.Repository.Abstracts;
public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllMovieAsync();
    Task<Movie> GetByIdMovieAsync(int? id);
    Task<Movie> AddMovieAsync(Movie movie);
    Task UpdateMovieAsync(int id, Movie movie);
    Task DeleteMovieAsync(int id);
}