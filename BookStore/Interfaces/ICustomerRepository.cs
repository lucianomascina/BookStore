using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<List<Customer>> GetAllOrderedByName();
    }
}
