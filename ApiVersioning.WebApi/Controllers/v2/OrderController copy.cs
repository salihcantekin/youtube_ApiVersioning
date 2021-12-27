using ApiVersioning.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVersioning.WebApi.Controllers.v2
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class OrderController: ControllerBase
    {
        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            var list = OrderDataService.GetOrdersv2();
            
            return Ok(list);
        }
    }
}
