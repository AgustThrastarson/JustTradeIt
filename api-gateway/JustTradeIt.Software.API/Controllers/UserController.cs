using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{identifier}")]
        public IActionResult GetUserInformation(string identifier)
        {
            return Ok(_userService.GetUserInformation(identifier));
        }
        
        [HttpGet]
        [Route("{identifier}/trades")]
        public IActionResult GetUserTrades(string identifier)
        {
            return Ok(_userService.GetUserTrades(identifier));
        }
    }
}