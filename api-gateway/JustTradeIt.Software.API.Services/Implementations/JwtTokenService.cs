using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly ITokenRepository _tokenRepository;
        public JwtTokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }
        public bool IsTokenBlacklisted(int tokenId)
        {
            return _tokenRepository.IsTokenBlacklisted(tokenId);
        }
    }
}