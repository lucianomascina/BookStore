using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Context;
using BookStore.Models;
using BookStore.Interfaces;

namespace BookStore
{
    public class GenresController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepository.GetAllOrderedByName();
            return View(genres);
        }

        public async Task<IActionResult> Details(int id)
        {

            var genre = await _genreRepository.GetById(id);
                
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreRepository.Add(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }


        public async Task<IActionResult> Edit(int id)
        {


            var genre = await _genreRepository.GetById(id); ;
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _genreRepository.Update(genre);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await GenreExists(genre.Id) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

 
        public async Task<IActionResult> Delete(int id)
        {

            var genre = await _genreRepository.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _genreRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GenreExists(int id)
        {
            var genres = await _genreRepository.GetAll();
            return genres.Any(e => e.Id == id);
        }
    }
}
