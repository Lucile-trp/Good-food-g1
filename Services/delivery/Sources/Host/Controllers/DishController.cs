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
        private readonly ILogger<DishController> _logger;
        private readonly IMapper _mapper;

        public DishController(IDishService dishService, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _dishService = dishService;
            _logger = loggerFactory.CreateLogger<DishController>();
            _mapper = mapper;
        }

        // GET 
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DishDto>))]
        public IActionResult GetDishesV1()
        {
            var dishDtos = GetDishDtos();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dishDtos);
        }

        // GET
        [HttpGet("{orderId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DishDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetDishByOrderIdV1(int orderId)
        {
            var dishDtos = GetDishDtos();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dishDtos.Where(d => d.OrderId == orderId));
        }

        private IEnumerable<DishDto> GetDishDtos() {
            var dishes = _dishService.GetDishes();
            var dishDtos = _mapper.Map<IEnumerable<DishDto>>(dishes);

            for (int i = 0; i < dishes.Count; i++)
            {
                dishDtos.ElementAt(i).OrderId = dishes.ElementAt(i).Order?.OrderId ?? 0;
            }

            return dishDtos;
        }
    }
}
