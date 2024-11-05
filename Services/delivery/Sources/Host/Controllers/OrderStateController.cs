using Microsoft.AspNetCore.Mvc;
using Host.Enums;
using Host.Helpers;
using Asp.Versioning;

namespace Host.Controllers.Enums
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderStateController : ControllerBase
    {
        [HttpGet]
        [ApiVersion("1.0")]
        public IActionResult Get()
        {
            var orderStates = Enum.GetValues(typeof(OrderState))
                .Cast<OrderState>()
                .Select(orderState => new
                {
                    Value = orderState,
                    DisplayName = orderState.GetDisplayName()
                })
                .ToList();
                
            return Ok(orderStates);
        }
    }
}

