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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        // GET 
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SupplierDto>))]
        public IActionResult GetSuppliersV1()
        {
            var suppliers = _supplierService.GetSuppliers();
            var supplierDtos = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(supplierDtos);
        }

        [HttpGet("{supplierId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(SupplierDto))]
        [ProducesResponseType(404)]
        public IActionResult GetSupplierByIdV1(int supplierId)
        {
            if (!_supplierService.SupplierExists(supplierId))
                return NotFound("Supplier not found.");

            var supplier = _supplierService.GetSupplierById(supplierId);
            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(supplierDto);
        }

        // CREATE
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateSupplierV1([FromBody] SupplierDto supplierCreate)
        {
            if (supplierCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var supplierEntity = _mapper.Map<Supplier>(supplierCreate);

            var success = _supplierService.CreateSupplier(supplierEntity);
            if (!success)
                return BadRequest("Error creating the delivery address.");

            return StatusCode(201, "Successfully created");
        }

        // UPDATE 
        [HttpPut("{supplierId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSupplierV1(int supplierId, [FromBody] SupplierDto supplierUpdate)
        {
            if (supplierUpdate == null || supplierId <= 0)
                return BadRequest(ModelState);

            if (!_supplierService.SupplierExists(supplierId))
                return NotFound("Supplier not found.");

            var supplierEntity = _mapper.Map<Supplier>(supplierUpdate);
            supplierEntity.SupplierId = supplierId;

            var success = _supplierService.UpdateSupplier(supplierEntity);
            if (!success)
                return BadRequest("Error updating the delivery address.");

            return NoContent();
        }

        // DELETE 
        [HttpDelete("{supplierId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSupplierV1(int supplierId)
        {
            if (!_supplierService.SupplierExists(supplierId))
                return NotFound("Supplier not found.");

            var success = _supplierService.DeleteSupplier(supplierId);
            if (!success)
                return BadRequest("Error deleting the delivery address.");

            return NoContent();
        }
    }
}
