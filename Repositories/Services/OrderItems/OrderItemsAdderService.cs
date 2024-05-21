using Microsoft.Extensions.Logging;
using Project.API.ServiceContracts.OrderItems;
using Project.Core.Domain.Entities;
using Project.Core.Domain.RepositoryContract;
using Project.Core.DTO;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.Services.OrderItems
{
	public class OrderItemsAdderService : IOrderItemsAdderService
	{
		private readonly ILogger<OrderItemsAdderService> logger;
		private readonly IOrderItemsRepository orderItemsRepository;

		public OrderItemsAdderService(ILogger<OrderItemsAdderService> logger, IOrderItemsRepository orderItemsRepository)
        {
			this.logger = logger;
			this.orderItemsRepository = orderItemsRepository;
		}

        public async Task<OrderItemResponse> AddOrderItemAsync(OrderItemAddRequest orderItemRequest)
		{
			logger.LogInformation("Adding order item");

			OrderItem orderItem = orderItemRequest.ToOrderItem();
			OrderItem addedOrderItem = await orderItemsRepository.AddOrderItemAsync(orderItem);
			OrderItemResponse orderItemResponse = addedOrderItem.ToOrderItemResponse();

			logger.LogInformation("Order item with Id {id} is added successfully", orderItemResponse.OrderItemId);

			return orderItemResponse;
		}
	}
}
