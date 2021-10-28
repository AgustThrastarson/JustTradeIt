using JustTradeIt.Software.API.Models.Dtos;

namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface ITokenService
    {
         string GenerateJwtToken(UserDto user);
    }
}