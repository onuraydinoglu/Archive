using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using ArchiveApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ArchiveApp.Repository.Concretes;
public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAllMovieAsync()
    {
        var movies = await _context.Movies.Include(x => x.Category).ToListAsync();
        return movies;
    }

    public async Task<Movie> GetByIdMovieAsync(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie is null)
        {
            throw new Exception($"Aradığınız id: {id} bulunamadı.");
        }
        return movie;
    }

    public async Task<Movie> AddMovieAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task UpdateMovieAsync(int id, Movie movie)
    {
        var mvi = await GetByIdMovieAsync(id);
        mvi.Name = movie.Name;
        mvi.Description = movie.Description;
        mvi.State = movie.State;
        mvi.CategoryId = movie.CategoryId;
        _context.Movies.Update(mvi);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = await GetByIdMovieAsync(id);
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }
}