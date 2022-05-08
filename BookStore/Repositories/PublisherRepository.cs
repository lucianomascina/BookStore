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
    public class PublisherRepository : BaseRepository<Publisher, BookStoreContext>, IPublisherRepository
    {
        public PublisherRepository(BookStoreContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Publisher>> GetAllOrderedByName()
        {
            return await DbSet.OrderBy(p => p.Name).ToListAsync();
        }
    }
}
