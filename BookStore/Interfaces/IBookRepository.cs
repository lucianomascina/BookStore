using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<Book> GetBook(int id);
        Task<List<Book>> GetAllOrderedByTitle();
    }
}
