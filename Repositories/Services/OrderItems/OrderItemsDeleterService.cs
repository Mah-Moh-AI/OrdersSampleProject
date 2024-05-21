using Microsoft.Extensions.Logging;
using Project.API.ServiceContracts.OrderItems;
using Project.Core.Domain.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.Services.OrderItems
{
	public class OrderItemsDeleterService : IOrderItemsDeleterService
	{
		private readonly ILogger<OrderItemsDeleterService> logger;
		private readonly IOrderItemsRepository orderItemsRepository;

		public OrderItemsDeleterService(ILogger<OrderItemsDeleterService> logger, IOrderItemsRepository orderItemsRepository)
        {
			this.logger = logger;
			this.orderItemsRepository = orderItemsRepository;
		}
        public async Task<bool> DeleteOrderItemByOrderItemIdAsync(Guid orderItemId)
		{
			logger.LogInformation("Deleting order item with Id {id}", orderItemId);

			bool isDeleted = await orderItemsRepository.DeleteOrderItemByOrderItemIdAsync(orderItemId);

			if(isDeleted)
			{
				logger.LogInformation("Order Item with Id {id} id deleted", orderItemId);
			}
			else
			{
				logger.LogInformation("Order Item with Id {id} not found for deletion", orderItemId);
			}

			return isDeleted;
		}
	}
}
