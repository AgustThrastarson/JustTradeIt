using System.Collections.Generic;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITradeRepository _tradeRepository;

        public UserService(IUserRepository userRepository, ITradeRepository tradeRepository)
        {
            _userRepository = userRepository;
            _tradeRepository = tradeRepository;
        }

        public UserDto GetUserInformation(string identifier)
        {
            return _userRepository.GetUserInformation(identifier);
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            return _tradeRepository.GetUserTrades(userIdentifier);
        } 
    }
}