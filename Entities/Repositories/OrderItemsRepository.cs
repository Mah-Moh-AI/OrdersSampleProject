using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core.Domain.Entities;
using Project.Core.Domain.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
	{
		private readonly ILogger<OrderItemsRepository> logger;
		private readonly ApplicationDbContext db;

		public OrderItemsRepository(ILogger<OrderItemsRepository> logger, ApplicationDbContext db)
        {
			this.logger = logger;
			this.db = db;
		}

        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
		{
			logger.LogInformation("Adding order item to database ...");

			db.OrderItems.Add(orderItem);

			await db.SaveChangesAsync();

			logger.LogInformation("Order Item with Id {id} is added to database", orderItem.OrderId);

			return orderItem;
		}

		public async Task<bool> DeleteOrderItemByOrderItemIdAsync(Guid orderItemId)
		{
			logger.LogInformation("Deleting order item to database ...");

			OrderItem? orderItem = await db.OrderItems.FindAsync(orderItemId);

			if (orderItem == null)
			{
				logger.LogWarning("Order item with Id {id} is not found in database", orderItemId);
				return false;
			}
			db.OrderItems.Remove(orderItem);

			await db.SaveChangesAsync();

			logger.LogInformation("Order item with Id {id} is deleted from database", orderItemId);

			return true;
		}

		public async Task<List<OrderItem>> GetAllOrderItemsAsync()
		{
			logger.LogInformation("Retrieving all orders items from database...");

			List<OrderItem> orderItemss = await db.OrderItems.ToListAsync();

			logger.LogInformation("All orders items are retrieved from database");

			return orderItemss;
		}

		public async Task<OrderItem?> GetOrderItemByOrderItemIdAsync(Guid orderItemId)
		{
			logger.LogInformation("Retrieving order item from database by Id {id}...", orderItemId);

			OrderItem? orderItem = await db.OrderItems.FindAsync(orderItemId);

			if (orderItem == null)
			{
				logger.LogWarning("Order item Id {id} is not found in database", orderItemId);
			}
			else
			{
				logger.LogInformation("Order item with Id {id} is retrieved from database successfully", orderItemId);
			}

			return orderItem;
		}

		public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId)
		{
			logger.LogInformation("Retrieving order items with order Id {id} from database...", orderId);

			List<OrderItem> OrderItems = await db.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();

			logger.LogInformation("Order items with order Id {id} is retrieved from database successfully", orderId);

			return OrderItems;
		}

		public async Task<OrderItem> UpdateOrderItemAsync(Guid orderItemId, OrderItem orderItem)
		{
			logger.LogInformation("Updating order item with Id {id} from database...", orderItemId);

			OrderItem? existingOrderItem = await db.OrderItems.FindAsync(orderItemId);
			if (existingOrderItem == null)
			{
				logger.LogWarning("Order Id {id} is not found in database", orderItemId);
				return orderItem; // to be checked if null should be returned
			}

			existingOrderItem.ProductName = orderItem.ProductName;
			existingOrderItem.UnitPrice = orderItem.UnitPrice;
			existingOrderItem.Quantity = orderItem.Quantity;
			existingOrderItem.TotalPrice = orderItem.TotalPrice;

			await db.SaveChangesAsync();

			logger.LogInformation("Order item with Id {id} is updated in database successfully", orderItemId);

			return existingOrderItem;
		}
	}
}
