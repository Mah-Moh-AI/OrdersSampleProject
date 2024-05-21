using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Project.Core.Domain.RepositoryContract;
using Project.Core.Domain.Entities;

namespace Project.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
	{
		private readonly ILogger<OrdersRepository> logger;
		private readonly ApplicationDbContext db;

		public OrdersRepository(ILogger<OrdersRepository> logger, ApplicationDbContext db)
        {
			this.logger = logger;
			this.db = db;
		}

        public async Task<Order> AddOrderAsync(Order order)
		{
			logger.LogInformation("Adding order to database ...");

			db.Orders.Add(order);

			await db.SaveChangesAsync();

			logger.LogInformation("Order with Id {id} is added to database", order.OrderId);

			return order;
		}

		public async Task<bool> DeleteOrderByOrderIdAsync(Guid orderId)
		{
			logger.LogInformation("Deleting order to database ...");

			Order? order = await db.Orders.FindAsync(orderId);

			if(order == null)
			{
				logger.LogWarning("Order with Id {id} is not found in database", orderId);
				return false;
			}
			db.Orders.Remove(order);

			await db.SaveChangesAsync();

			logger.LogInformation("Order with Id {id} is deleted from database", orderId);

			return true;
		}

		public async Task<List<Order>> GetAllOrdersAsync()
		{
			logger.LogInformation("Retrieving all orders from database...");

			List<Order> orders = await db.Orders.Include("Items").ToListAsync();

			logger.LogInformation("All orders are retrieved from database");

			return orders;
		}

		public async Task<List<Order>> GetFilteredOrdersAsync(Expression<Func<Order, bool>> predicate) // to bec studied
		{
			logger.LogInformation("Retrieving filtered orders...");

			List<Order> filteredOrders = await db.Orders.Where(predicate).ToListAsync();

			logger.LogInformation("Filtered Orders retrieved from database successfully");

			return filteredOrders;
		}

		public async Task<Order?> GetOrderByIdAsync(Guid orderId)
		{
			logger.LogInformation("Retrieving order from database by Id {id}...", orderId);

			Order? order = await db.Orders.FindAsync(orderId);

			if(order == null)
			{
				logger.LogWarning("Order Id {id} is not found in database", orderId);
			}
			else
			{
				logger.LogInformation("Order with Id {id} is retrieved from database successfully", orderId);
			}

			return order;
		}

		public async Task<Order> UpdateOrderASync(Guid orderId, Order order)
		{
			logger.LogInformation("Updating order with Id {id} from database...", orderId);

			Order? existingOrder = await db.Orders.FindAsync(orderId);
			if (existingOrder == null)
			{
				logger.LogWarning("Order Id {id} is not found in database", orderId);
				return order;
			}

			existingOrder.OrderNumber = order.OrderNumber;
			existingOrder.OrderDate = order.OrderDate;
			existingOrder.CustomerName = order.CustomerName;
			existingOrder.TotalAmount = order.TotalAmount;

			await db.SaveChangesAsync();

			logger.LogInformation("Order with Id {id} is updated in database successfully", orderId);

			return existingOrder;

		}
	}
}
