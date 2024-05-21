using Project.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.ServiceContracts.OrderItems
{
	public interface IOrderItemsUpdaterService
	{
		Task<OrderItemResponse> UpdateOrderItemAsync(Guid orderItemId, OrderItemUpdateRequest orderItemRequest);
	}
}
