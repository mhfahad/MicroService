using EF.Core.Repository.Interface.Repository;
using Ordering.Domain.Models;
using System.Threading.Tasks;


namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : ICommonRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string UserName);
        Task<bool> CreateOrder(Order UserName);
    }
}
