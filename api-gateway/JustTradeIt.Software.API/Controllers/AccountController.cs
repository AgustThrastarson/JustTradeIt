using System;
using System.Linq;
using System.Security.Claims;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        private ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        
        
        [AllowAnonymous]
        [HttpPost]
        [Route("register")] // "/api/account/register"
        public IActionResult Register([FromBody] RegisterInputModel user)
        {
            if (!ModelState.IsValid) { return BadRequest("Invalid ModelState"); }

            var account = _accountService.CreateUser(user);
            var token = _tokenService.GenerateJwtToken(account);
            return Ok(token);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")] // "/api/account/login"
        public IActionResult Login([FromBody] LoginInputModel user)
        {
            var logincred = _accountService.AuthenticateUser(user);
            if (logincred == null)
            {
                return Unauthorized("Invalid email or password :( ");
            }
            return Ok(_tokenService.GenerateJwtToken(logincred));
        }


        [HttpGet]
        [Route("logout")]
        public IActionResult LogOut()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            return Ok(_accountService.GetProfileInformation(name));
        }

        [HttpPut]
        [Route("profile")]
        public IActionResult EditProfile([FromForm] ProfileInputModel user)
        {
            if (!ModelState.IsValid) { return BadRequest("Invalid ModelState"); }
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            _accountService.UpdateProfile(email, user);
            return NoContent();
        }
        
    }
}
