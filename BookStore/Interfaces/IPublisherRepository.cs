using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Interfaces
{
    public interface IPublisherRepository : IBaseRepository<Publisher>
    {
        Task<List<Publisher>> GetAllOrderedByName();
    }
}
