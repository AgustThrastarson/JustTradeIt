using Newtonsoft.Json;

namespace JustTradeIt.Software.API.Models
{
    public class ExceptionModel
    {
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}