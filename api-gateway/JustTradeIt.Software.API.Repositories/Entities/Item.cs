using System;
using System.Collections.Generic;

namespace JustTradeIt.Software.API.Repositories.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int ItemConditionId { get; set; }
        
        public bool Deleted { get; set; }
        
        
        //Nav Prop
        public ItemCondition ItemConditionNav{ get; set; }
        public User OwnerId { get; set; }
        public ICollection<TradeItem> TradeItems { get; set; }
        public ICollection<ItemImage> ItemImages { get; set; }

    }
}