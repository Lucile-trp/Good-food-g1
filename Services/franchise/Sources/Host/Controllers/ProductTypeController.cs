using Microsoft.AspNetCore.Mvc;
using Host.Enums;
using Host.Helpers;
using Asp.Versioning;

namespace Host.Controllers.Enums
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        [HttpGet]
        [ApiVersion("1.0")]
        public IActionResult Get()
        {
            var productTypes = Enum.GetValues(typeof(ProductType))
                .Cast<ProductType>()
                .Select(productType => new
                {
                    Value = productType,
                    DisplayName = productType.GetDisplayName()
                })
                .ToList();
                
            return Ok(productTypes);
        }
    }
}

