using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Project.Core.Domain.Entities;

namespace Project.Core.DTO
{
	public class OrderAddRequest
	{
		[Required(ErrorMessage = "The OrderNumber field is required.")]
		[RegularExpression(@"^(?i)ORD_\d{4}_\d+$", ErrorMessage = "The Order number should begin with 'ORD' followed by an underscore (_) and a sequential number.")]
		public string? OrderNumber { get; set; }

		[Required(ErrorMessage = "The Customer Name is required.")]
		[StringLength(50, ErrorMessage = "The Customer Name field must not exceed 50 characters.")]
		public string? CustomerName { get; set; }

		[Required(ErrorMessage = "The OrderDate field is required.")]
		public DateTime OrderDate { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "The Total amount must be a positive number.")]
		[Column(TypeName = "decimal")]
		public decimal TotalAmount { get; set; }

		public List<OrderItemAddRequest> OrderItems { get; set; } = new List<OrderItemAddRequest>();

		/// <summary>
		/// Convert OrderAddRequest DTO to Order Entity
		/// </summary>
		/// <returns>Order Entity</returns>
		public Order ToOrder()
		{
			return new Order
			{
				OrderNumber = OrderNumber,
				CustomerName = CustomerName,
				OrderDate = OrderDate,
				TotalAmount = TotalAmount
			};
		}
	}
}
