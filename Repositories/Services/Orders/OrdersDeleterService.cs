using Microsoft.Extensions.Logging;
using Project.API.ServiceContracts.Orders;
using Project.Core.Domain.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.Services.Orders
{
	public class OrdersDeleterService : IOrdersDeleterService
	{
		private readonly ILogger<OrdersDeleterService> logger;
		private readonly IOrdersRepository ordersRepository;

		public OrdersDeleterService(ILogger<OrdersDeleterService> logger, IOrdersRepository ordersRepository)
        {
			this.logger = logger;
			this.ordersRepository = ordersRepository;
		}

        public async Task<bool> DeleteOrderByIdAsync(Guid orderId)
		{
			logger.LogInformation("Deleting order with Id: {id}", orderId);

			bool isDeleted = await ordersRepository.DeleteOrderByOrderIdAsync(orderId);

			if (isDeleted)
			{
				logger.LogInformation("Order with Id {id} deleted successfully", orderId);
			}
			else
			{
				logger.LogWarning("Order with Id {id} not found", orderId);
			}

			return isDeleted;
		}
	}
}
