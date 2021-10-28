using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ProfileInputModel
    {
        public string FullName { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}