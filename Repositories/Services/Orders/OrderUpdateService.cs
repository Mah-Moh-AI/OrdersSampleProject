using Microsoft.Extensions.Logging;
using Project.API.ServiceContracts.Orders;
using Project.Core.Domain.Entities;
using Project.Core.Domain.RepositoryContract;
using Project.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.Services.Orders
{
	public class OrderUpdateService : IOrdersUpdaterService
	{
		private readonly ILogger<OrderUpdateService> logger;
		private readonly IOrdersRepository ordersRepository;

		public OrderUpdateService(ILogger<OrderUpdateService> logger, IOrdersRepository ordersRepository)
        {
			this.logger = logger;
			this.ordersRepository = ordersRepository;
		}
        public async Task<OrderResponse> UpdateOrderAsync(Guid id, OrderUpdateRequest orderUpdateRequest)
		{
			logger.LogInformation("Updating order Id {id}", id);

			Order order = orderUpdateRequest.ToOrder();
			Order UpdatedOrder = await ordersRepository.UpdateOrderASync(id, order);
			OrderResponse orderResponse = UpdatedOrder.ToOrderResponse();

			logger.LogInformation("Order with Id {id} is updated successfully", id);

			return orderResponse;
		}
	}
}
