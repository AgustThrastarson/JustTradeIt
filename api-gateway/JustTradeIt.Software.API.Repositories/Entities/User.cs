using System.Collections.Generic;

namespace JustTradeIt.Software.API.Repositories.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public string HashedPassword { get; set; }
        
        //Nav Prop
         public ICollection<Item> Items { get; set; }
         public ICollection<TradeItem> TradeItems { get; set; }
         public virtual ICollection<Trade> TradeSent {get; set; }
         public virtual ICollection<Trade> TradeReceived {get; set; }

    }
}