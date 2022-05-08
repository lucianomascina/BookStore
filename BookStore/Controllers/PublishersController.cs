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
    public class PublishersController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

     
        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherRepository.GetAllOrderedByName();
            return View(publishers);
        }

       
        public async Task<IActionResult> Details(int id)
        {


            var publisher = await _publisherRepository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

     
        public IActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                await _publisherRepository.Add(publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        public async Task<IActionResult> Edit(int id)
        {
        

            var publisher = await _publisherRepository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country")] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _publisherRepository.Update(publisher);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await PublisherExists(publisher.Id) == false)
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
            return View(publisher);
        }

    
        public async Task<IActionResult> Delete(int id)
        {

            var publisher = await _publisherRepository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _publisherRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PublisherExists(int id)
        {
            var publishers = await _publisherRepository.GetAll();
            return publishers.Any(e => e.Id == id);
        }
    }
}
