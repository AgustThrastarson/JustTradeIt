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
        public IActionResult GetTrades([FromQuery] bool onlyCompleted=false,[FromQuery] bool onlyIncludeActive=false)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            if (onlyCompleted)
            {
                return Ok(_tradeService.GetTrades(email));
            }
            return Ok(_tradeService.GetTradeRequests(email, onlyIncludeActive));
        }

        [HttpGet]
        [Route("{identifier}", Name = "get_trade_by_identifier")]
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
            return CreatedAtRoute(routeName: "get_trade_by_identifier",routeValues: new{identifier = name}, name);
        }

        [HttpPatch]
        [Route("{identifier}")]
        public IActionResult PatchTrade(string identifier, [FromBody] string status)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            _tradeService.UpdateTradeRequest(identifier, email, status);
            return NoContent();

        }
    }
}