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
    public class GenreRepository : BaseRepository<Genre, BookStoreContext>, IGenreRepository
    {
        public GenreRepository(BookStoreContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Genre>> GetAllOrderedByName()
        {
            return await DbSet.OrderBy(g => g.Name).ToListAsync();
        }
    }
}
