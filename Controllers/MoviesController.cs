using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveApp.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var movies = await _movieRepository.GetAllMovieAsync();
        return View(movies);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Movie movie)
    {
        await _movieRepository.AddMovieAsync(movie);

        return RedirectToAction("Index");
    }

}