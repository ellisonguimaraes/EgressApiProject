using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EgressProject.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("testapi")]
        public IActionResult GetaAsync()
        {
            return Ok("Rota acessada!");
        }
    }
}