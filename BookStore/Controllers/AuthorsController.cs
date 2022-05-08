using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Context;
using BookStore.Models;
using BookStore.Repositories;
using BookStore.Interfaces;


namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {

        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
           
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAllOrderedByName();
            return View(authors);
         
        }  
        public async Task<IActionResult> Details(int id)
        {

            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDate")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorRepository.Add(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }


        public async Task<IActionResult> Edit(int id)
        {


            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BirthDate")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _authorRepository.Update(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await AuthorExists(author.Id) == false)
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
            return View(author);
        }

     
        public async Task<IActionResult> Delete(int id)
        {


            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AuthorExists(int id)
        {
            var authors = await _authorRepository.GetAll();
            return authors.Any(e => e.Id == id);
        }
    }
}
