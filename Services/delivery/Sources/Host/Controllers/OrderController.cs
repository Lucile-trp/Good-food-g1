using Host.Interfaces.Services;
using Host.Dto;
using Host.Enums;
using Host.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Asp.Versioning;
using RabbitMQ.Connection;
using RabbitMQ.EventBus;
using RabbitMQ;
using Host.Dto.Rpc;

namespace Host.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService; 
        private readonly IDeliveryAddressService _deliveryAddressService; 
        private readonly IMapper _mapper;
        private readonly IRabbitMQEventBus _eventBusGetDish;

        public OrderController(IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory,
            IOrderService orderService, 
            IUserService userService, 
            IDeliveryAddressService deliveryAddressService, 
            IMapper mapper)
        {
            _orderService = orderService;
            _userService = userService;
            _deliveryAddressService = deliveryAddressService;
            _mapper = mapper;

            var persistentConnection = serviceProvider.GetServices<IHostedService>().OfType<IRabbitMQPersistentConnection>().Single();
            _eventBusGetDish = new RabbitMQEventBus(persistentConnection, loggerFactory, Queues.SendDish);
        }

        // GET 
        [HttpGet]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public IActionResult GetOrdersV1()
        {
            var orders = _orderService.GetOrders();
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDtos);
        }

        // GET (by id)
        [HttpGet("{orderId}")]
        [MapToApiVersion("1")]
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

        [HttpGet("ByCustomer/{customerId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public IActionResult GetOrdersByCustomerV1(int customerId)
        {
            var orders = _orderService.GetOrdersByCustomer(customerId);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDtos);
        }

        [HttpGet("byDeliverer/{delivererId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public IActionResult GetOrdersByDelivererV1(int delivererId)
        {
            var orders = _orderService.GetOrdersByDeliverer(delivererId);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDtos);
        }

        [HttpGet("ByDeliveryAddress/{deliveryAddressId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public IActionResult GetOrdersByDeliveryAddressV1(int deliveryAddressId)
        {
            var orders = _orderService.GetOrdersByDeliveryAddress(deliveryAddressId);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDtos);
        }

        [HttpGet("ByState/{state}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public IActionResult GetOrdersByStateV1(int state)
        {
            var orders = _orderService.GetOrdersByState(state);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDtos);
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrderV1([FromBody] OrderSendDto order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _eventBusGetDish.Publish(order);
            }
            catch (System.Exception)
            {
                return BadRequest("Error sending the order.");
            }

            return Ok();
        }



        [HttpPut("{orderId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrderV1(int orderId, [FromBody] OrderRecvDto orderUpdate)
        {
            if (orderUpdate == null || orderId <= 0)
                return BadRequest(ModelState);

            if (!_orderService.OrderExists(orderId))
                return NotFound("Order not found.");

            var existingOrder = _orderService.GetOrderByIdAsNoTracking(orderId);

            if (existingOrder == null)
                return NotFound("Order not found.");

            var deliverer = _userService.GetUserByIdAsNoTracking(orderUpdate.DeliveryId);
            if (deliverer == null)
                return BadRequest("Deliverer not found.");

            var deliveryAddress = _deliveryAddressService.GetDeliveryAddressByIdAsNoTracking(orderUpdate.DeliveryAdresseId);
            if (deliveryAddress == null)
                return BadRequest("Delivery address not found.");

            var orderEntity = _mapper.Map<Order>(orderUpdate);
            orderEntity.OrderId = orderId;
            orderEntity.Date = orderEntity.Date; 
            orderEntity.Customer = existingOrder.Customer; 
            orderEntity.Deliverer = deliverer;  
            orderEntity.DeliveryAddress = deliveryAddress; 

            var success = _orderService.UpdateOrder(orderEntity);
            if (!success)
                return BadRequest("Error updating the order.");

            return NoContent();
        }


        // DELETE
        [HttpDelete("{orderId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrderV1(int orderId)
        {
            if (!_orderService.OrderExists(orderId))
                return NotFound("Order not found.");

            var success = _orderService.DeleteOrder(orderId);
            if (!success)
                return BadRequest("Error deleting the order.");

            return NoContent();
        }
    }
}
