using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.ServiceContracts.OrderItems;
using Project.Core.DTO;


namespace Project.API.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemsGetterService orderItemsGetterService;
        private readonly IOrderItemsAdderService orderItemsAdderService;
        private readonly IOrderItemsUpdaterService orderItemsUpdaterService;
        private readonly IOrderItemsDeleterService orderItemsDeleterService;
        private readonly ILogger<OrderItemsController> logger;

        public OrderItemsController(
            IOrderItemsGetterService orderItemsGetterService,
            IOrderItemsAdderService orderItemsAdderService,
            IOrderItemsUpdaterService orderItemsUpdaterService,
            IOrderItemsDeleterService orderItemsDeleterService,
            ILogger<OrderItemsController> logger
            )
        {
            this.orderItemsGetterService = orderItemsGetterService;
            this.orderItemsAdderService = orderItemsAdderService;
            this.orderItemsUpdaterService = orderItemsUpdaterService;
            this.orderItemsDeleterService = orderItemsDeleterService;
            this.logger = logger;
        }


        /// <summary>
        /// Retrieve Order Items by Order Id
        /// </summary>
        /// <param name="orderId">Order Id to retrieve its items</param>
        /// <returns>List of Order Items</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<OrderItemResponse>>> GetOrderItemsByOrderId([FromQuery] Guid orderId)
        {
            logger.LogInformation("Retrieving order items by order id {id}", orderId);

            List<OrderItemResponse> orderItems = await orderItemsGetterService.GetOrderItemsByOrderIdAsync(orderId);

            logger.LogInformation("Order items of order id {id} are retrieved successfully", orderId);

            return Ok(orderItems);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItemById([FromRoute] Guid id)
        {
            logger.LogInformation($"Retrieving order item Id: {id}");

            OrderItemResponse? orderItem = await orderItemsGetterService.GetOrderItemByOrderItemIdAsync(id);

            if (orderItem == null)
            {
                logger.LogWarning($"Order Item with Id {id} not found");
                return NotFound();
            }

            logger.LogInformation($"Order Item with Id {id} retrieved successfully");

            return Ok(orderItem);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrderItemResponse>> AddOrderItem([FromBody] OrderItemAddRequest orderAddRequest)
        {
            logger.LogInformation("Adding a new order");

            OrderItemResponse addedOrderItem = await orderItemsAdderService.AddOrderItemAsync(orderAddRequest);

            logger.LogInformation($"Order with Id {addedOrderItem.OrderItemId} added successfully");

            return CreatedAtAction(nameof(GetOrderItemById), new { id = addedOrderItem.OrderItemId }, addedOrderItem);
        }


        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderItemResponse>> UpdateOrderItem([FromRoute] Guid id, [FromBody] OrderItemUpdateRequest orderItemUpdateRequest)
        {
            logger.LogInformation("Updating existing order item Id: {id}", id);

            OrderItemResponse updatedOrderItem = await orderItemsUpdaterService.UpdateOrderItemAsync(id, orderItemUpdateRequest);

            logger.LogInformation("Order item Id {id} updated successfully", updatedOrderItem.OrderId);

            return Ok(updatedOrderItem);
        }


        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOrderItem([FromRoute] Guid id)
        {
            logger.LogInformation("Deleting order item with Id: {id}", id);

            bool isDeleted = await orderItemsDeleterService.DeleteOrderItemByOrderItemIdAsync(id);

            if (!isDeleted)
            {
                logger.LogWarning("Order item with Id {id} is not found", id);
                return NotFound();
            }

            logger.LogInformation("Order item Id {id} is deleted", id);
            return NoContent();

        }
    }
}
