using BookStore.Context;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class CustomerRepository : BaseRepository<Customer, BookStoreContext>, ICustomerRepository
    {
        public CustomerRepository(BookStoreContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Customer>> GetAllOrderedByName()
        {
            return await DbSet.OrderBy(c => c.LastName).ToListAsync();
        }
    }
}
