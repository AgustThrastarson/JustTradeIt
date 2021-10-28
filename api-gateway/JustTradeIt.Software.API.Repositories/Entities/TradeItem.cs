using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTradeIt.Software.API.Repositories.Entities
{
    public class TradeItem
    {
        public int TradeId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        
        //Nav Prop
        public virtual Trade Trade { get; set; }
        public virtual User User { get; set; }
        public virtual Item Item { get; set; }
        
    }
}