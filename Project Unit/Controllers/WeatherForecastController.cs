using Microsoft.AspNetCore.Mvc;

namespace Project_Unit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        public string Get()
        {
            return "hi";
        }
    }
}