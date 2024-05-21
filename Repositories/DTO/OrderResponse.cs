using Project.Core.Domain.Entities;
using Project.Core.Domain.RepositoryContract;
using System;
using System.Collections.Generic;


namespace Project.Core.DTO
{
	public class OrderResponse
	{
		public Guid OrderId { get; set; }

		public string? OrderNumber {  get; set; }

		public string? CustomerName { get; set; }

		public DateTime OrderDate {  get; set; }

		public decimal TotalAmount { get; set; }

		public List<OrderItemResponse> OrderItems { get; set; } = new List<OrderItemResponse>();

		public Order ToOrder()
		{
			return new Order
			{

				OrderId = OrderId,
				OrderNumber = OrderNumber,
				CustomerName = CustomerName,
				OrderDate = OrderDate,
				TotalAmount = TotalAmount

			};
		}
	}

	public static class OrderExtensions
	{
		public static OrderResponse ToOrderResponse(this Order order)
		{
			return new OrderResponse
			{
				OrderId = order.OrderId,
				OrderNumber = order.OrderNumber,
				CustomerName = order.CustomerName,
				OrderDate = order.OrderDate,
				TotalAmount = order.TotalAmount,
				OrderItems = order.Items.Select(item => new OrderItemResponse
				{
					ProductName = item.ProductName,
					Quantity = item.Quantity,
					UnitPrice = item.UnitPrice,
					TotalPrice = item.TotalPrice,
				}).ToList()

			};
		}

		public static List<OrderResponse> ToOrderResponseList(this List<Order> orders)
		{
			List<OrderResponse> orderResponses = new List<OrderResponse>();

			foreach (var order in orders)
			{
				orderResponses.Add(ToOrderResponse(order));
			}

			return orderResponses;
		}
	}

}
