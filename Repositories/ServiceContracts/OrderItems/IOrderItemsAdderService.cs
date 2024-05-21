using Project.Core.DTO;
using System;
using System.Collections.Generic;

namespace Project.API.ServiceContracts.OrderItems
{
	public interface IOrderItemsAdderService
	{
		Task<OrderItemResponse> AddOrderItemAsync(OrderItemAddRequest orderItemRequest);

	}
}
