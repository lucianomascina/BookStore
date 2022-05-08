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
    public class BookRepository : BaseRepository<Book,BookStoreContext>, IBookRepository
    {
        public BookRepository(BookStoreContext dbContext) : base(dbContext)
        {
        }

        public async Task<Book> GetBook(int id)
        {
            return await DbSet.Include(a => a.Author).Include(p => p.Publisher).Include(g => g.Genre).
                FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<List<Book>> GetAllOrderedByTitle()
        {
            return await DbSet.Include(a => a.Author).Include(p => p.Publisher).Include(g => g.Genre)
                .OrderBy(b => b.Title).ToListAsync();
           
        }
    }
}
