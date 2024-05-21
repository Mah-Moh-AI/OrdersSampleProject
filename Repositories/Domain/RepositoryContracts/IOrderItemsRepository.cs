using Project.Core.Domain.Entities;
using System;
using System.Collections.Generic;


namespace Project.Core.Domain.RepositoryContract
{
	public interface IOrderItemsRepository
	{
		Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);

		Task<bool> DeleteOrderItemByOrderItemIdAsync(Guid orderItemId);

		Task<List<OrderItem>> GetAllOrderItemsAsync();
		 
		Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId);

		Task<OrderItem?> GetOrderItemByOrderItemIdAsync(Guid orderItemId);

		Task<OrderItem> UpdateOrderItemAsync(Guid orderItemId, OrderItem orderItem);
	}
}
