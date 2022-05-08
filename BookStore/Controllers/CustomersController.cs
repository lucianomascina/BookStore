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

namespace BookStore.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

      
        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllOrderedByName();
            return View(customers);
        }
  
        public async Task<IActionResult> Details(int id)
        {


            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.Add(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

   
        public async Task<IActionResult> Edit(int id)
        {
          
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    await _customerRepository.Update(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await CustomerExists(customer.Id) == false)
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
            return View(customer);
        }

      
        public async Task<IActionResult> Delete(int id)
        {


            var customer = await _customerRepository.GetById(id);
               
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CustomerExists(int id)
        {
            var customers = await _customerRepository.GetAll();
            return customers.Any(e => e.Id == id);
        }
    }
}
