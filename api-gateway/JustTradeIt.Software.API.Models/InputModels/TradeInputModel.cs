using System.Collections.Generic;
using JustTradeIt.Software.API.Models.Dtos;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class TradeInputModel
    {
        public string ReceiverIdentifier { get; set; }
        public IEnumerable<ItemDto> ItemsInTrade { get; set; }
        
    }
}