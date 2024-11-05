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
    [ApiVersion("1.0")]
    [ApiController]
    public class DeliveryAddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _deliveryAddressService;
        private readonly IMapper _mapper;

        public DeliveryAddressController(IDeliveryAddressService deliveryAddressService, IMapper mapper)
        {
            _deliveryAddressService = deliveryAddressService;
            _mapper = mapper;
        }

        // GET 
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DeliveryAddressDto>))]
        public IActionResult GetDeliveryAddressesV1()
        {
            var deliveryAddresses = _deliveryAddressService.GetDeliveryAddresses();
            var deliveryAddressDtos = _mapper.Map<IEnumerable<DeliveryAddressDto>>(deliveryAddresses);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(deliveryAddressDtos);
        }

        [HttpGet("{deliveryAddressId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(DeliveryAddressDto))]
        [ProducesResponseType(404)]
        public IActionResult GetDeliveryAddressByIdV1(int deliveryAddressId)
        {
            if (!_deliveryAddressService.DeliveryAddressExists(deliveryAddressId))
                return NotFound("DeliveryAddress not found.");

            var deliveryAddress = _deliveryAddressService.GetDeliveryAddressById(deliveryAddressId);
            var deliveryAddressDto = _mapper.Map<DeliveryAddressDto>(deliveryAddress);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(deliveryAddressDto);
        }

        // CREATE
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateDeliveryAddressV1([FromBody] DeliveryAddressDto deliveryAddressCreate)
        {
            if (deliveryAddressCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deliveryAddressEntity = _mapper.Map<DeliveryAddress>(deliveryAddressCreate);

            var success = _deliveryAddressService.CreateDeliveryAddress(deliveryAddressEntity);
            if (!success)
                return BadRequest("Error creating the delivery address.");

            return StatusCode(201, "Successfully created");
        }

        // UPDATE 
        [HttpPut("{deliveryAddressId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeliveryAddressV1(int deliveryAddressId, [FromBody] DeliveryAddressDto deliveryAddressUpdate)
        {
            if (deliveryAddressUpdate == null || deliveryAddressId <= 0)
                return BadRequest(ModelState);

            if (!_deliveryAddressService.DeliveryAddressExists(deliveryAddressId))
                return NotFound("DeliveryAddress not found.");

            var deliveryAddressEntity = _mapper.Map<DeliveryAddress>(deliveryAddressUpdate);
            deliveryAddressEntity.DeliveryAddressId = deliveryAddressId;

            var success = _deliveryAddressService.UpdateDeliveryAddress(deliveryAddressEntity);
            if (!success)
                return BadRequest("Error updating the delivery address.");

            return NoContent();
        }

        // DELETE 
        [HttpDelete("{deliveryAddressId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDeliveryAddressV1(int deliveryAddressId)
        {
            if (!_deliveryAddressService.DeliveryAddressExists(deliveryAddressId))
                return NotFound("DeliveryAddress not found.");

            var success = _deliveryAddressService.DeleteDeliveryAddress(deliveryAddressId);
            if (!success)
                return BadRequest("Error deleting the delivery address.");

            return NoContent();
        }
    }
}
