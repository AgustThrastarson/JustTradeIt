using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JustTradeIt.Software.API.Models.Enums;

namespace JustTradeIt.Software.API.Repositories.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public DateTime IssuerDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public TradeStatus TradeStatus { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        
        //Nav Prop
        public ICollection<TradeItem> TradeItems { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }

         

    }
}