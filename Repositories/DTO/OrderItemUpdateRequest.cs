using Project.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Core.DTO
{
	public class OrderItemUpdateRequest
	{
		[Required(ErrorMessage = "The OrderId field is required.")]
		public Guid OrderId { get; set; }

		[Required(ErrorMessage = "The ProductName field is required.")]
		[StringLength(50, ErrorMessage = "The ProductName field must not exceed 50 characters.")]
		public string? ProductName { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "The Quantity field must be a positive number.")]
		public int Quantity { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "The UnitPrice field must be a positive number.")]
		public decimal UnitPrice { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "The total price of the order item.")]
		public decimal TotalPrice { get; set; }

		public OrderItem ToOrderItem()
		{
			return new OrderItem
			{
				OrderId = OrderId,
				ProductName = ProductName,
				Quantity = Quantity,
				UnitPrice = UnitPrice,
				TotalPrice = TotalPrice,
			};
		}
	}
}
