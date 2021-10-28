using System.Collections.Generic;

namespace JustTradeIt.Software.API.Repositories.Entities
{
    public class ItemCondition
    {
        public int Id { get; set; }
        public string ConditionCode { get; set; }
        public string Description { get; set; }
        
        //Nav Prop
        public ICollection<Item> Items { get; set; }
    }
}