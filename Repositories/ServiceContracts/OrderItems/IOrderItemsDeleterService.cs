using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.ServiceContracts.OrderItems
{
	public interface IOrderItemsDeleterService
	{
		Task<bool> DeleteOrderItemByOrderItemIdAsync(Guid orderItemId);
	}
}
