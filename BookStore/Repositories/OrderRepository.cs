using BookStore.Context;
using BookStore.Interfaces;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class OrderRepository : BaseRepository<Order, BookStoreContext>, IOrderRepository
    {
        public OrderRepository(BookStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
