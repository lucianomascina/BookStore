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
        public async Task<IActionResult> Create(PostBook postBook)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Author = await _authorRepository.GetById(postBook.AuthorId),
                    Publisher = await _publisherRepository.GetById(postBook.PublisherId),
                    Genre = await _genreRepository.GetById(postBook.GenreId),
                    Title = postBook.Title,
                    Isbn = postBook.Isbn,
                    Stock = postBook.Stock,
                    Price = postBook.Price,
                    PublicationDate = postBook.PublicationDate
                };
                await _bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            return View(postBook);
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
            var postBook = new PostBook
            {
                Id = book.Id,
                AuthorId = book.Author.Id,
                GenreId = book.Genre.Id,
                Isbn = book.Isbn,
                Price = book.Price,
                PublicationDate = book.PublicationDate,
                PublisherId = book.Publisher.Id,
                Stock = book.Stock,
                Title = book.Title 
            };

            return View(postBook);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,PostBook postBook)
        {
            var book = await _bookRepository.GetById(id);

            if (ModelState.IsValid)
            {
                try
                {
                    book.Author = await _authorRepository.GetById(postBook.AuthorId);
                    book.Publisher = await _publisherRepository.GetById(postBook.PublisherId);
                    book.Genre = await _genreRepository.GetById(postBook.GenreId);
                    book.Title = postBook.Title;
                    book.Isbn = postBook.Isbn;
                    book.Stock = postBook.Stock;
                    book.Price = postBook.Price;
                    book.PublicationDate = postBook.PublicationDate;

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
