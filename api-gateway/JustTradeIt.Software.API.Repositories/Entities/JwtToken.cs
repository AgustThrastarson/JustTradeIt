using System;

namespace JustTradeIt.Software.API.Repositories.Entities
{
    public class JwtToken
    {
        public int Id { get; set; }
        public Boolean BlackListed { get; set; }
    }
}