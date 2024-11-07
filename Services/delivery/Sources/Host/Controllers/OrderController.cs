using Host.Core;
using Host.Interfaces.Services;
using Host.Dto;
using Host.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Asp.Versioning;
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
        private readonly IUserService _userService; 
        private readonly IDeliveryAddressService _deliveryAddressService; 
        private readonly IMapper _mapper;

        public OrderController(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IOrderService orderService, IUserService userService, IDeliveryAddressService deliveryAddressService, IMapper mapper)
        {
            _orderService = orderService;
            _userService = userService;
            _deliveryAddressService = deliveryAddressService;
            _mapper = mapper;

            //var persistentConnection = serviceProvider.GetServices<IHostedService>().OfType<IRabbitMQPersistentConnection>().Single();
            //eventBus = new RabbitMQEventBus(persistentConnection, loggerFactory, Queues.Order);
            //eventBus.Subscribe(new OrderHandler(persistentConnection, loggerFactory));
        }

        // GET (all orders)
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
        public IActionResult CreateOrderV1([FromQuery] int customerId, [FromQuery] int delivererId, [FromQuery] int deliveryAddressId, [FromBody] OrderDto orderCreate)
        {
            if (orderCreate == null)
                return BadRequest("Invalid order data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = _userService.GetUserById(customerId);
            if (customer == null)
                return BadRequest("Customer not found.");

            var deliverer = _userService.GetUserById(delivererId);
            if (deliverer == null)
                return BadRequest("Deliverer not found.");

            // Vérification de l'existence de l'adresse de livraison
            var deliveryAddress = _deliveryAddressService.GetDeliveryAddressById(deliveryAddressId);
            if (deliveryAddress == null)
            {
                // Si l'adresse de livraison n'existe pas, retourner une erreur ou en créer une nouvelle
                return BadRequest("Delivery address not found.");
            }

            // Si l'adresse de livraison existe, on peut l'utiliser pour la commande
            var orderEntity = _mapper.Map<Order>(orderCreate);
            orderEntity.Customer = customer;
            orderEntity.Deliverer = deliverer;
            orderEntity.DeliveryAddress = deliveryAddress;
            orderEntity.Date = DateTime.UtcNow;

            var success = _orderService.CreateOrder(orderEntity);
            if (!success)
                return BadRequest("Error creating the order.");

            return StatusCode(201, "Successfully created");
        }



        [HttpPut("{orderId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrderV1(int orderId, [FromQuery] int delivererId, [FromQuery] int deliveryAddressId, [FromBody] OrderDto orderUpdate)
        {
            if (orderUpdate == null || orderId <= 0)
                return BadRequest(ModelState);

            if (!_orderService.OrderExists(orderId))
                return NotFound("Order not found.");

            var existingOrder = _orderService.GetOrderByIdAsNoTracking(orderId);

            if (existingOrder == null)
                return NotFound("Order not found.");

            var deliverer = _userService.GetUserByIdAsNoTracking(delivererId);
            if (deliverer == null)
                return BadRequest("Deliverer not found.");

            var deliveryAddress = _deliveryAddressService.GetDeliveryAddressByIdAsNoTracking(deliveryAddressId);
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
