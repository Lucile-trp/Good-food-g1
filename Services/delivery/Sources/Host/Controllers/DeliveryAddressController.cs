using Host.Interfaces.Services;
using Host.Dto;
using Host.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asp.Versioning;
using System.Collections.Generic;

namespace Host.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class DeliveryAddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _deliveryAddressService;
        private readonly IUserService _userService; 
        private readonly IMapper _mapper;

        public DeliveryAddressController(IDeliveryAddressService deliveryAddressService, IUserService userService, IMapper mapper)
        {
            _deliveryAddressService = deliveryAddressService;
            _userService = userService; 
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DeliveryAddressDto>))]
        public IActionResult GetDeliveryAddressesV1()
        {
            var deliveryAddresses = _deliveryAddressService.GetDeliveryAddresses();
            if (deliveryAddresses == null)
                return NotFound("No delivery addresses found.");

            var deliveryAddressDtos = _mapper.Map<IEnumerable<DeliveryAddressDto>>(deliveryAddresses);
            return Ok(deliveryAddressDtos);
        }

        [HttpGet("{deliveryAddressId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(DeliveryAddressDto))]
        [ProducesResponseType(404)]
        public IActionResult GetDeliveryAddressByIdV1(int deliveryAddressId)
        {
            if (!_deliveryAddressService.DeliveryAddressExists(deliveryAddressId))
                return NotFound("Delivery address not found.");

            var deliveryAddress = _deliveryAddressService.GetDeliveryAddressById(deliveryAddressId);
            var deliveryAddressDto = _mapper.Map<DeliveryAddressDto>(deliveryAddress);
            return Ok(deliveryAddressDto);
        }

        [HttpGet("ByCustomer/{customerId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DeliveryAddressDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetDeliveryAddressesByCustomerV1(int customerId)
        {
            var deliveryAddresses = _deliveryAddressService.GetDeliveryAddressesByCustomer(customerId);
            if (deliveryAddresses == null || deliveryAddresses.Count == 0)
                return NotFound("No delivery addresses found for this customer.");

            var deliveryAddressDtos = _mapper.Map<IEnumerable<DeliveryAddressDto>>(deliveryAddresses);
            return Ok(deliveryAddressDtos);
        }

        [HttpGet("ByOrder/{orderId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(200, Type = typeof(DeliveryAddressDto))]
        [ProducesResponseType(404)]
        public IActionResult GetDeliveryAddressByOrderV1(int orderId)
        {
            var deliveryAddress = _deliveryAddressService.GetDeliveryAddressByOrder(orderId);

            if (deliveryAddress == null)
                return NotFound("Delivery address not found for this order.");

            var deliveryAddressDto = _mapper.Map<DeliveryAddressDto>(deliveryAddress);
            return Ok(deliveryAddressDto);
        }

        [HttpPost("customer/{customerId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateDeliveryAddressV1(int customerId, [FromBody] DeliveryAddressDto deliveryAddressCreate)
        {
            if (deliveryAddressCreate == null)
                return BadRequest("Invalid data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = _userService.GetUserById(customerId);
            if (customer == null)
                return NotFound("Customer not found.");

            var deliveryAddressEntity = _mapper.Map<DeliveryAddress>(deliveryAddressCreate);
            deliveryAddressEntity.Customer = customer; 

            if (!_deliveryAddressService.CreateDeliveryAddress(deliveryAddressEntity))
                return StatusCode(500, "A problem occurred while handling your request.");

            return StatusCode(201, "Successfully created");
        }

        [HttpPut("{deliveryAddressId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeliveryAddressV1(int deliveryAddressId, [FromBody] DeliveryAddressDto deliveryAddressUpdate)
        {
            if (deliveryAddressUpdate == null || deliveryAddressId <= 0)
                return BadRequest("Invalid data.");

            if (!_deliveryAddressService.DeliveryAddressExists(deliveryAddressId))
                return NotFound("Delivery address not found.");

            var existingAddress = _deliveryAddressService.GetDeliveryAddressById(deliveryAddressId);
            if (existingAddress == null)
                return NotFound("Delivery address not found.");

            var deliveryAddressEntity = _mapper.Map<DeliveryAddress>(deliveryAddressUpdate);
            deliveryAddressEntity.DeliveryAddressId = deliveryAddressId;

            deliveryAddressEntity.Customer = existingAddress.Customer;

            if (!_deliveryAddressService.UpdateDeliveryAddress(deliveryAddressEntity))
                return StatusCode(500, "A problem occurred while updating the delivery address.");

            return NoContent();
        }

        [HttpDelete("{deliveryAddressId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDeliveryAddressV1(int deliveryAddressId)
        {
            if (!_deliveryAddressService.DeliveryAddressExists(deliveryAddressId))
                return NotFound("Delivery address not found.");

            if (!_deliveryAddressService.DeleteDeliveryAddress(deliveryAddressId))
                return StatusCode(500, "A problem occurred while deleting the delivery address.");

            return NoContent();
        }
    }
}
