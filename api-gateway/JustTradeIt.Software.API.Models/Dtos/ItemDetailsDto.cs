using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.Dtos
{
    public class ItemDetailsDto
    {
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<ImageDto> Images { get; set; }
        public int NumberOfActiveTradeRequests { get; set; }
        public string Condition { get; set; }
        public string Owner { get; set; }
    }
}