using System;
using System.Linq;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        private ITokenService _tokenService;

        public TradeController(ITradeService tradeService, ITokenService tokenService)
        {
            _tradeService = tradeService;
            _tokenService = tokenService;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult GetTrades()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            return Ok(_tradeService.GetTrades(email));

        }

        [HttpGet]
        [Route("{identifier}")]
        public IActionResult GetTrade(string identifier)
        {
            return Ok(_tradeService.GetTradeByIdentifer(identifier));
        }
        
        [HttpPost]
        [Route("")]
        public IActionResult PostTrade([FromBody] TradeInputModel trade)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            var name = _tradeService.CreateTradeRequest(email, trade);
            return StatusCode(201, name);
        }

        [HttpPatch]
        [Route("{identifier}")]
        public IActionResult PatchTrade(string identifier, [FromBody] string status)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            return Ok(_tradeService.UpdateTradeRequest(identifier, email, status));

        }
    }
}