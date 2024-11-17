using Host.Interfaces.Services;
using Host.Dto;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asp.Versioning;
using Host.Models;

namespace Host.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly IMapper _mapper;

        public DishController(IDishService dishService, IMapper mapper)
        {
            _dishService = dishService;
            _mapper = mapper;
        }

        // GET 
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DishDto>))]
        public IActionResult GetDishesV1()
        {
            var dishes = _dishService.GetDishes();
            var dishDtos = dishes.Select(d => new DishDto {
                Cost = d.Cost,
                Description = d.Description,
                Id = d.Id,
                OrderId = d.Order.OrderId,
                RestaurantId = d.RestaurantId,
                Title = d.Title
            });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dishDtos);
        }

        // GET
        [HttpGet("{orderId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DishDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetDishByOrderIbV1(int orderId)
        {
            var dishes = _dishService.GetDishes();
            var dishDtos = _mapper.Map<IEnumerable<DishDto>>(dishes);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dishDtos.Where(d => d.OrderId == orderId));
        }
    }
}
