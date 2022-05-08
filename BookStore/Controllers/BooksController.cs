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
using BookStore.ViewModels;

namespace BookStore
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IGenreRepository _genreRepository;
      
        public BooksController(IBookRepository bookRepository,IAuthorRepository authorRepository,
            IPublisherRepository publisherRepository,IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _genreRepository = genreRepository;
        }

     
        public async Task<IActionResult> Index()
        {
            
            return View(await _bookRepository.GetAllOrderedByTitle());
        }

   
        public async Task<IActionResult> Details(int id)
        {
         

            var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

     
        public async Task<IActionResult> Create()
        {
            ViewData["Authors"] = new SelectList(await _authorRepository.GetAllOrderedByName(), "Id", "LastName");
            ViewData["Publishers"] = new SelectList(await _publisherRepository.GetAllOrderedByName(),"Id","Name");
            ViewData["Genres"] = new SelectList(await _genreRepository.GetAllOrderedByName(), "Id", "Name");

            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostBook postbook)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Author = await _authorRepository.GetById(postbook.AuthorId),
                    Publisher = await _publisherRepository.GetById(postbook.PublisherId),
                    Genre = await _genreRepository.GetById(postbook.GenreId),
                    Title = postbook.Title,
                    Isbn = postbook.Isbn,
                    Stock = postbook.Stock,
                    Price = postbook.Price,
                    PublicationDate = postbook.PublicationDate
                };
                await _bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            return View(postbook);
        }

       
        public async Task<IActionResult> Edit(int id)
        {

            var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["Authors"] = new SelectList(await _authorRepository.GetAllOrderedByName(), "Id", "LastName");
            ViewData["Publishers"] = new SelectList(await _publisherRepository.GetAllOrderedByName(), "Id", "Name");
            ViewData["Genres"] = new SelectList(await _genreRepository.GetAllOrderedByName(), "Id", "Name");
            return View(book);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Isbn,Stock,Price,PublicationDate,Image")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookRepository.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await BookExists(book.Id) == false)
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
            return View(book);
        }

       
        public async Task<IActionResult> Delete(int id)
        {

            var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookExists(int id)
        {
            var books = await _bookRepository.GetAll();
            return books.Any(e => e.Id == id);
        }
    }
}
