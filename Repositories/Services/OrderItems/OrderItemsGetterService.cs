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
	public class OrderItemsGetterService : IOrderItemsGetterService
	{
		private readonly ILogger<OrderItemsGetterService> logger;
		private readonly IOrderItemsRepository orderItemsRepository;

		public OrderItemsGetterService(ILogger<OrderItemsGetterService> logger, IOrderItemsRepository orderItemsRepository)
        {
			this.logger = logger;
			this.orderItemsRepository = orderItemsRepository;
		}

        public async Task<List<OrderItemResponse>> GetAllOrderItemsAsync()
		{
			logger.LogInformation("Getting All Order Items ...");

			List<OrderItem> orderItems = await orderItemsRepository.GetAllOrderItemsAsync();

			List<OrderItemResponse> orderItemsResponse = orderItems.ToOrderItemResponseList();

			logger.LogInformation("All Orders Items are retrieved successfully");

			return orderItemsResponse;
		}

		public async Task<OrderItemResponse?> GetOrderItemByOrderItemIdAsync(Guid orderItemId)
		{
			logger.LogInformation("Getting Order Item by Id {id} ...", orderItemId);

			OrderItem? orderItem = await orderItemsRepository.GetOrderItemByOrderItemIdAsync(orderItemId);
			OrderItemResponse? orderItemResponse = orderItem?.ToOrderItemResponse();

			if(orderItemResponse != null)
			{
				logger.LogWarning("Order item not found for Order Item Id: {id}", orderItemId);
			}
			else
			{
				logger.LogInformation("Order Item with Id {id} is retrieved successfully", orderItemId);
			}

			return orderItemResponse;

		}

		public async Task<List<OrderItemResponse>> GetOrderItemsByOrderIdAsync(Guid orderId)
		{
			logger.LogInformation("Getting Order Items by order Id {id} ...", orderId);

			List<OrderItem> orderItems = await orderItemsRepository.GetOrderItemsByOrderIdAsync(orderId);
			List<OrderItemResponse> orderItemResponses = orderItems.ToOrderItemResponseList();

			logger.LogInformation("Order Items with order Id {id} are retrieved successfully", orderId);

			return orderItemResponses;
		}
	}
}
