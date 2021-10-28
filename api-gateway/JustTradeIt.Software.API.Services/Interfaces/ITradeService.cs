using System.Collections.Generic;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;

namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface ITradeService
    {
        /// <summary>
        /// Gets all successful trades for a particular user. After a trade has been finalized it cannot be altered.
        /// </summary>
        /// <param name="email">The email of the user which participated in the trade</param>
        /// <returns>A list of all trades</returns>
         IEnumerable<TradeDto> GetTrades(string email);
         /// <summary>
         /// Get a trade by a unique identifier (GUID) 
         /// </summary>
         /// <param name="tradeIdentifier">The unique identifier for the trade</param>
         /// <returns>A single detailed trade object</returns>
         TradeDetailsDto GetTradeByIdentifer(string tradeIdentifier);
         /// <summary>
         /// Gets all trade requests for a particular user
         /// </summary>
         /// <param name="email">The email of the user with trade requests</param>
         /// <param name="onlyIncludeActive">Determines whether to only include active trade requests or include active, pending, timed out and cancelled trade requests as well.</param>
         /// <returns>A list of trade requests</returns>
         IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive = true);
         /// <summary>
         /// Create a new trade request
         /// </summary>
         /// <param name="email">The email of the user initiating the request</param>
         /// <param name="tradeRequest">Information regarding the request</param>
         /// <returns>Identifier of the new trade request</returns>
         string CreateTradeRequest(string email, TradeInputModel tradeRequest);

         /// <summary>
         /// Changes the status of a trade request
         /// </summary>
         /// <param name="identifier">The identifier of the trade request</param>
         /// <param name="email">The email of the user changing the request</param>
         /// <param name="status">Resulting status of the trade request</param>
         object UpdateTradeRequest(string identifier, string email, string status);
    }
}