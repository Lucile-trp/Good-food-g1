using Host.Core;
using Host.Handlers;
using Host.Interfaces.Services;
using Host.Dto;
using Host.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asp.Versioning;
using System.Collections.Generic;
using RabbitMQ.Connection;
using RabbitMQ.EventBus;

namespace Host.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IRabbitMQEventBus eventBus;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;

            var persistentConnection = serviceProvider.GetServices<IHostedService>().OfType<IRabbitMQPersistentConnection>().Single();
            eventBus = new RabbitMQEventBus(persistentConnection, loggerFactory, Queues.Order);
            eventBus.Subscribe(new OrderHandler(persistentConnection, loggerFactory));
        }

        //RABBITMQ
        [HttpPost("send")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public ActionResult Send(OrderDto order)
        {
            eventBus.Publish(order);
            return Ok();
        }

        // GET 
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public IActionResult GetOrdersV1()
        {
            var orders = _orderService.GetOrders();
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDtos);
        }

        [HttpGet("{orderId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(404)]
        public IActionResult GetOrderByIdV1(int orderId)
        {
            if (!_orderService.OrderExists(orderId))
                return NotFound("Order not found.");

            var order = _orderService.GetOrderById(orderId);
            var orderDto = _mapper.Map<OrderDto>(order);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDto);
        }

        // CREATE
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrderV1([FromBody] OrderDto orderCreate)
        {
            if (orderCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderEntity = _mapper.Map<Order>(orderCreate);

            var success = _orderService.CreateOrder(orderEntity);
            if (!success)
                return BadRequest("Error creating the delivery address.");

            return StatusCode(201, "Successfully created");
        }

        // UPDATE 
        [HttpPut("{orderId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrderV1(int orderId, [FromBody] OrderDto orderUpdate)
        {
            if (orderUpdate == null || orderId <= 0)
                return BadRequest(ModelState);

            if (!_orderService.OrderExists(orderId))
                return NotFound("Order not found.");

            var orderEntity = _mapper.Map<Order>(orderUpdate);
            orderEntity.OrderId = orderId;

            var success = _orderService.UpdateOrder(orderEntity);
            if (!success)
                return BadRequest("Error updating the delivery address.");

            return NoContent();
        }

        // DELETE 
        [HttpDelete("{orderId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrderV1(int orderId)
        {
            if (!_orderService.OrderExists(orderId))
                return NotFound("Order not found.");

            var success = _orderService.DeleteOrder(orderId);
            if (!success)
                return BadRequest("Error deleting the delivery address.");

            return NoContent();
        }
    }
}
