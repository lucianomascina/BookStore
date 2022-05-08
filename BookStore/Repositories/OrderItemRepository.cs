using BookStore.Context;
using BookStore.Interfaces;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem, BookStoreContext>, IOrderItemRepository
    {
        public OrderItemRepository(BookStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
