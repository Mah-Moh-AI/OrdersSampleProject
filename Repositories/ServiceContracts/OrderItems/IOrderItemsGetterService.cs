using Project.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.ServiceContracts.OrderItems
{
	public interface IOrderItemsGetterService
	{
		Task<List<OrderItemResponse>> GetAllOrderItemsAsync();

		Task<List<OrderItemResponse>> GetOrderItemsByOrderIdAsync(Guid orderId);

		Task<OrderItemResponse?> GetOrderItemByOrderItemIdAsync(Guid orderItemId);
	}
}
