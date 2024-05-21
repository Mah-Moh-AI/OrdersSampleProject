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
	public class OrdersAdderService : IOrdersAdderService
	{
		private readonly ILogger<OrdersAdderService> logger;
		private readonly IOrdersRepository ordersRepository;
		private readonly IOrderItemsRepository orderItemsRepository;

		public OrdersAdderService(ILogger<OrdersAdderService> logger, IOrdersRepository ordersRepository, IOrderItemsRepository orderItemsRepository)
		{
			this.logger = logger;
			this.ordersRepository = ordersRepository;
			this.orderItemsRepository = orderItemsRepository;
		}
		public async Task<OrderResponse> AddOrderAsync(OrderAddRequest orderRequest)
		{
			logger.LogInformation("Adding a new order from OrdersAdderService");

			Order order = orderRequest.ToOrder();

			Order addedOrder = await ordersRepository.AddOrderAsync(order);

			OrderResponse orderResponse = addedOrder.ToOrderResponse();

			logger.LogInformation("Order added successfully");

			return orderResponse;
		}
	}
}
