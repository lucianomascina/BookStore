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
    public class AuthorRepository : BaseRepository<Author,BookStoreContext>, IAuthorRepository
    {
        public AuthorRepository(BookStoreContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Author>> GetAllOrderedByName()
        {
            return await DbSet.OrderBy(a => a.LastName).ToListAsync();

        }
    }
}
