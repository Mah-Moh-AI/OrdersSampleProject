using Project.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.ServiceContracts.Orders
{
	public interface IOrdersAdderService
	{
		Task<OrderResponse> AddOrderAsync(OrderAddRequest orderAddRequest);
	}
}
