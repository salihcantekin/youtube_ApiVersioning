using ApiVersioning.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVersioning.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrderController: ControllerBase
    {
        [HttpGet("GetOrders")]
        [MapToApiVersion("1.0")]
        public IActionResult GetOrders()
        {
            var list = OrderDataService.GetOrders();
            
            return Ok(list);
        }
    }
}
