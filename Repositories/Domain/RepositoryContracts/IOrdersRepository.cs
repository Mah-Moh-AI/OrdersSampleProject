using Project.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Project.Core.Domain.RepositoryContract
{
	public interface IOrdersRepository
	{
		Task<List<Order>> GetAllOrdersAsync();

		Task<Order?> GetOrderByIdAsync(Guid orderId);

		Task<List<Order>> GetFilteredOrdersAsync(Expression<Func<Order, bool>> predicate); // to be sudied

		Task<Order> AddOrderAsync(Order order);

		Task<Order> UpdateOrderASync(Guid orderId, Order order);

		Task<bool> DeleteOrderByOrderIdAsync(Guid orderId);

	}
}
