﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.API.ServiceContracts.Orders
{
	public interface IOrdersDeleterService
	{
		Task<bool> DeleteOrderByIdAsync(Guid orderId);
	}
}
