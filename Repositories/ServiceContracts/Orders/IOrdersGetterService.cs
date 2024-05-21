using Project.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.ServiceContracts.Orders
{
	public interface IOrdersGetterService
	{
		/// <summary>
		/// Retrieve all orders.
		/// </summary>
		/// <returns>A list of orders.</returns>
		Task<List<OrderResponse>> GetAllOrdersAsync();

		/// <summary>
		/// Retrieves an order by it's Id.
		/// </summary>
		/// <param name="orderId">The Id of the order to retrieve.</param>
		/// <returns>The retrieved order or null if not found.</returns>
		Task<OrderResponse?> GetOrderByIdAsync(Guid orderId);

	}
}
