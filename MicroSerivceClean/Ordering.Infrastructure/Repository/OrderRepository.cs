using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Models;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repository
{
    public class OrderRepository : CommonRepository<Order>, IOrderRepository
    {
        OrderDbContext _dbContext;
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateOrder(Order UserName)
        {
            await _dbContext.Orders.AddAsync(UserName);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders.Where(c => c.UserName.ToLower() == userName.ToLower()).ToListAsync();
            return orderList;
        }
    }
}