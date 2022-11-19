using Microsoft.AspNetCore.Mvc;
using Unit_Data.Models;

namespace Project_Unit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        UnitDbContext _context;

        public UserController(ILogger<UserController> logger, UnitDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserVM model)
        {
            return Ok(model);
        }

        [HttpGet("GetWeatherForecast")]
        public string Get()
        {
            return "hi";
        }
    }
}