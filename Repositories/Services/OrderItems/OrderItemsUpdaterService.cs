using Microsoft.Extensions.Logging;
using Project.API.ServiceContracts.OrderItems;
using Project.Core.Domain.Entities;
using Project.Core.Domain.RepositoryContract;
using Project.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.Services.OrderItems
{
	public class OrderItemsUpdaterService : IOrderItemsUpdaterService
	{
		private readonly ILogger<OrderItemsUpdaterService> logger;
		private readonly IOrderItemsRepository orderItemsRepository;

		public OrderItemsUpdaterService(ILogger<OrderItemsUpdaterService> logger, IOrderItemsRepository orderItemsRepository)
        {
			this.logger = logger;
			this.orderItemsRepository = orderItemsRepository;
		}

        public async Task<OrderItemResponse> UpdateOrderItemAsync(Guid orderItemId, OrderItemUpdateRequest orderItemRequest)
		{
			logger.LogInformation("Updating Order Item wth Id {id}", orderItemId);

			OrderItem orderItem= orderItemRequest.ToOrderItem();

			OrderItem orderItemUpdated = await orderItemsRepository.UpdateOrderItemAsync(orderItemId, orderItem);

			OrderItemResponse orderItemResponse = orderItemUpdated.ToOrderItemResponse();

			logger.LogInformation("Order Item with Id {id} is updated successfully", orderItemId);

			return orderItemResponse;

		}
	}
}
