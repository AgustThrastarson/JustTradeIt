using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly string[] itemconditon = {"MINT", "GOOD", "USED", "BAD", "DAMAGED"};

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult GetItem([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] bool ascendingOrderby)
        {
            return Ok(_itemService.GetItems(pageSize, pageNumber, ascendingOrderby));
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateItem([FromBody] ItemInputModel item)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            if (!itemconditon.Contains(item.ConditionCode))
            {
                return BadRequest("Incorrect Condition Code");
            };
            var newitem = _itemService.AddNewItem(email, item);
            return CreatedAtRoute(routeName: "get_item_by_identifier",routeValues: new{identifier = newitem}, newitem);
        }

        [HttpGet]
        [Route("{identifier}", Name = "get_item_by_identifier")]
        public IActionResult GetDetailedItem(string identifier)
        {
            var item = _itemService.GetItemByIdentifier(identifier);
            return StatusCode(200, item);
        }

        [HttpDelete]
        [Route("{identifier}")]
        public IActionResult DeleteItem(string identifier)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            _itemService.RemoveItem(email, identifier);
            return NoContent();
        }
        
        
    }
}