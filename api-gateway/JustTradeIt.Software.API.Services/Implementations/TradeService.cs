using System;
using System.Collections.Generic;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IQueueService _queueService;
 
        public TradeService(ITradeRepository tradeRepository, IQueueService queueService)
        {
            _tradeRepository = tradeRepository;
            _queueService = queueService;
        }
        
        
        
        public string CreateTradeRequest(string email, TradeInputModel tradeRequest)
        {
            _queueService.PublishMessage("new-trade-request",email);   
            return _tradeRepository.CreateTradeRequest(email, tradeRequest);
        }

        public TradeDetailsDto GetTradeByIdentifer(string tradeIdentifier)
        {
            return _tradeRepository.GetTradeByIdentifier(tradeIdentifier);
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            return _tradeRepository.GetTrades(email);
        }

        public object UpdateTradeRequest(string email, string identifier , string status)
        {
            Enum.TryParse(status, out TradeStatus tradestatus);
            return _tradeRepository.UpdateTradeRequest(identifier, email, tradestatus);
        }
    }
}