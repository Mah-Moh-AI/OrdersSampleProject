using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.Filters.CustomActionFilters;
using Project.API.ServiceContracts.Orders;
using Project.Core.DTO;


namespace Project.API.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersGetterService ordersGetterService;
        private readonly IOrdersAdderService orderAdderService;
        private readonly IOrdersUpdaterService orderUpdateService;
        private readonly IOrdersDeleterService orderDeleterService;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(
            IOrdersGetterService ordersGetterService,
            IOrdersAdderService orderAdderService,
            IOrdersUpdaterService orderUpdateService,
            IOrdersDeleterService orderDeleterService,
            ILogger<OrdersController> logger
            )
        {
            this.ordersGetterService = ordersGetterService;
            this.orderAdderService = orderAdderService;
            this.orderUpdateService = orderUpdateService;
            this.orderDeleterService = orderDeleterService;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieve All orders
        /// </summary>
        /// <returns>A list of orders</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
        {
            logger.LogInformation("Retrieving all orders");

            List<OrderResponse> orders = await ordersGetterService.GetAllOrdersAsync();

            logger.LogInformation("All orders retrieved successfully");

            return orders;
        }

        /// <summary>
        /// Retrieve an order by id
        /// </summary>
        /// <param name="id">id to retrieve order from database</param>
        /// <returns>The retrieved order or not found if not found</returns>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponse>> GetOrderById([FromRoute] Guid id)
        {
            logger.LogInformation($"Retrieving order Id: {id}");

            OrderResponse? order = await ordersGetterService.GetOrderByIdAsync(id);

            if (order == null)
            {
                logger.LogWarning($"Order with Id {id} not found");
                return NotFound();
            }

            logger.LogInformation($"Order with Id {id} retrieved successfully");

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ValidateModel] // not accessed since Model validation is done during model binding prior to Filter
        public async Task<ActionResult<OrderResponse>> AddOrder([FromBody] OrderAddRequest orderAddRequest)
        {
            logger.LogInformation("Adding a new order");

            OrderResponse addedOrder = await orderAdderService.AddOrderAsync(orderAddRequest);

            logger.LogInformation($"Order with Id {addedOrder.OrderId} added successfully");

            return CreatedAtAction(nameof(GetOrderById), new { id = addedOrder.OrderId }, addedOrder);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "SystemAdmin, Admin")]
        [ValidateModel] // the error is sent back prior to validation. Check sequence
        public async Task<ActionResult<OrderResponse>> UpdateOrder([FromRoute] Guid id, [FromBody] OrderUpdateRequest orderUpdateRequest)
        {
            logger.LogInformation("Updating existing order Id: {id}", id);

            OrderResponse updatedOrder = await orderUpdateService.UpdateOrderAsync(id, orderUpdateRequest);

            logger.LogInformation("Order Id {id} updated successfully", updatedOrder.OrderId);

            return Ok(updatedOrder);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "SystemAdmin, Admin")]
        public async Task<ActionResult> DeleteOrder([FromRoute] Guid id)
        {
            logger.LogInformation("Deleting order with Id: {id}", id);

            bool isDeleted = await orderDeleterService.DeleteOrderByIdAsync(id);

            if (!isDeleted)
            {
                logger.LogWarning("Order with Id {id} is not found", id);
                return NotFound();
            }

            logger.LogInformation("Order Id {id} is deleted", id);
            return NoContent();

        }
    }
}
