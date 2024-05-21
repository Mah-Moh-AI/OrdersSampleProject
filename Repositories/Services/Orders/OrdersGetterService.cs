using Microsoft.Extensions.Logging;
using Project.API.ServiceContracts.OrderItems;
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
	public class OrdersGetterService : IOrdersGetterService
	{
		private readonly ILogger<OrdersGetterService> logger;
		private readonly IOrdersRepository ordersRepository;
		private readonly IOrderItemsGetterService orderItemsGetterService;

		public OrdersGetterService(
			ILogger<OrdersGetterService> logger,
			IOrdersRepository ordersRepository,
			IOrderItemsGetterService orderItemsGetterService
			)
        {
			this.logger = logger;
			this.ordersRepository = ordersRepository;
			this.orderItemsGetterService = orderItemsGetterService;
		}

		
        public async Task<List<OrderResponse>> GetAllOrdersAsync()
		{
			logger.LogInformation("Retrieving all orders from services");

			List<Order> orders = await ordersRepository.GetAllOrdersAsync();

			List<OrderResponse> ordersResponses = orders.ToOrderResponseList();

            foreach (OrderResponse orderResponse in ordersResponses)
            {
				orderResponse.OrderItems = await orderItemsGetterService.GetOrderItemsByOrderIdAsync(orderResponse.OrderId);
			}

            logger.LogInformation("Orders retrieved successfully from services");

			return ordersResponses;
		}


		public async Task<OrderResponse?> GetOrderByIdAsync(Guid orderId)
		{
			logger.LogInformation("Retrieving order with Id {id}", orderId);

			Order? order = await ordersRepository.GetOrderByIdAsync(orderId);

			if (order == null)
			{
				logger.LogWarning("Order with Id {id} not found", orderId);
				return null;
			}

			OrderResponse orderResponse = order.ToOrderResponse();
			orderResponse.OrderItems = await orderItemsGetterService.GetOrderItemsByOrderIdAsync(orderResponse.OrderId);

			logger.LogInformation("Order Id {id} is retrieved", orderId);

			return orderResponse;

		}
	}
}
