using Compass.Data.Data.ViewModels;
using Compass.Data.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unit_Data.Interface;
using Unit_Data.Models;
using Unit_Services.Services;

namespace Project_Unit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        UnitDbContext _context;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UnitDbContext context, UserService userService)
        {
            _logger = logger;
            _context = context;
            _userService = userService;
        }

        [HttpPost("Register"), AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserVM model)
        {
            var result = await _userService.RegisterUserAsync(model);

            return Ok(result);
        }
        
        [HttpPost("Login"), AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserVM model)
        {
            var result = await _userService.LoginUserAsync(model);

            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(TokenRequestVM model)
        {
            var validator = new TokenRequestValidation();
            var validatorResult = await validator.ValidateAsync(model);
            if (validatorResult.IsValid)
            {
                var result = await _userService.RefreshTokenAsync(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(validatorResult.Errors);
        }

        [HttpGet("GetAllUsers"),AllowAnonymous]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetAllChatsByUser"), AllowAnonymous]
        public async Task<IActionResult> GetAllChatsByUserAsync(GetChatsVM model)
        {
            var result = await _userService.GetAllChatsByUserAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddChat"), AllowAnonymous]
        public async Task<IActionResult> AddChatAsync(AddChatVM model)
        {
            var result = await _userService.AddChatAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetAllMessagesByChat"), AllowAnonymous]
        public async Task<IActionResult> GetAllMessagesByChatAsync(GetMessagesByChatIdVM model)
        {
            var result = await _userService.GetAllMessagesByChatAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddMessage"), AllowAnonymous]
        public async Task<IActionResult> AddMessageAsync(AddMessageVM model)
        {
            var result = await _userService.AddMessageAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        
        [HttpPost("AddContactToUser"), AllowAnonymous]
        public async Task<IActionResult> AddContactToUserAsync(AppUserContactVM model)
        {
            var result = await _userService.AddContactToUserAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetContactsByUser"),AllowAnonymous]
        public async Task<IActionResult> GetContactsByUser(GetContactsByUserVM model)
        {
            var result = await _userService.GetContactsByUserAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        
        [HttpPost("GetUserById"), AllowAnonymous]
        public async Task<IActionResult> GetUserById(GetUserByIdVM model)
        {
            var result = await _userService.GetUserById(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UploadImage"), AllowAnonymous]
        public async Task<IActionResult> UploadImage(UploadImageVM model)
        {
            var result = await _userService.UploadImage(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UploadAvatar"), AllowAnonymous]
        public async Task<IActionResult> UploadAvatar(UploadAvatarVM model)
        {
            var result = await _userService.UploadAvatar(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetAvatar"), AllowAnonymous]
        public async Task<IActionResult> GetAvatar(GetUserByIdVM model)
        {
            var result = await _userService.GetAvatar(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}