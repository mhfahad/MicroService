using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Qeuries.GetOrdersByUserName
{
    public class GetOrdersByUserQuery : IRequest<List<OrderVm>>
    {
        public string UserName { get; set; }
        public GetOrdersByUserQuery(string userName)
        {
            UserName = userName;
        }


    }
}
