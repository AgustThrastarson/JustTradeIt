using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.Exceptions;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Entities;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    
    public class TradeRepository : ITradeRepository
    {
        private readonly TradeItDbContext _dbContext;
        
        public TradeRepository(TradeItDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string CreateTradeRequest(string email, TradeInputModel trade)
        {
            var tradesender = _dbContext.Users.FirstOrDefault(x => x.Email == email);
            var tradereceiver = _dbContext.Users.FirstOrDefault(x => x.PublicIdentifier == trade.ReceiverIdentifier);
            if (tradereceiver == null)
            {
                throw new ResourceNotFoundException("Trade Receiver not Found :C ");
            }

            List<ItemDto> ItemListSender = new List<ItemDto>();
            List<ItemDto> ItemListReceiver = new List<ItemDto>();
            List<TradeItem> TradeItemList = new List<TradeItem>();


            foreach (var item in trade.ItemsInTrade)
            {
                
                var currItem = _dbContext.Items.Where(x=> x.Deleted != true).FirstOrDefault(i => i.PublicIdentifier == item.Identifier);
                if (currItem == null)
                {
                    throw new ResourceNotFoundException("Item not Found :C ");
                }
                
                if (currItem.OwnerId.PublicIdentifier == tradesender.PublicIdentifier)
                {
                    var tradeitem = new TradeItem
                    {
                        User = tradesender,
                        Item = currItem
                    };
                    TradeItemList.Add(tradeitem);
                    ItemListSender.Add(item);
                    
                }
                else if (currItem.OwnerId.PublicIdentifier == trade.ReceiverIdentifier)
                {
                    var tradeitem = new TradeItem
                    {
                        User = tradereceiver,
                        Item = currItem
                    };
                    TradeItemList.Add(tradeitem);
                    ItemListReceiver.Add(item);
                }
                else
                {
                    throw new ResourceNotFoundException("Item not Found :C ");
                }
                if (currItem == null)
                {
                    throw new ResourceNotFoundException("Invalid Owner of Item :( ");;
                }
            }
            
            if (tradereceiver == null && tradesender == null || tradereceiver == null || tradesender == null)
            {
                throw new ResourceNotFoundException("Invalid TradeIdentifier :( ");
            }

            if (ItemListSender.Count == 0 && ItemListReceiver.Count == 0 || ItemListSender.Count == 0 || ItemListReceiver.Count == 0 )
            {
                throw new ResourceNotFoundException("Invalid Items :( ");
            }

            var entity = new Trade
            {
                PublicIdentifier = Guid.NewGuid().ToString(),
                IssuerDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedBy = tradesender.FullName,
                TradeStatus = TradeStatus.Pending,
                SenderId = tradesender.Id,
                ReceiverId = tradereceiver.Id,
                TradeItems = TradeItemList,
                Receiver = tradereceiver,
                Sender = tradesender
            };
            _dbContext.Trades.Add(entity);
            _dbContext.SaveChanges();
            
            return entity.PublicIdentifier;
        }

        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {

            var tradeitem = _dbContext.Trades.FirstOrDefault(t => t.PublicIdentifier == identifier);
            if (tradeitem == null)
            {
                throw new ResourceNotFoundException("Trade not Found :C ");
            }

            var recitem = _dbContext.Items.Include(xo => xo.OwnerId)
                .Where(i => i.OwnerId.Id == tradeitem.ReceiverId).Select(c => new ItemDto
                {
                    Identifier = c.PublicIdentifier,
                    Title = c.Title,
                    ShortDescription = c.ShortDescription,
                    Owner = new UserDto
                    {
                        Identifier = c.OwnerId.PublicIdentifier,
                        FullName = c.OwnerId.FullName,
                        Email = c.OwnerId.Email,
                        ProfileImageUrl = c.OwnerId.ProfileImageUrl
                    }
                }).ToList();


            var offeringitem = _dbContext.Items.Include(it => it.OwnerId)
                .Where(i => i.OwnerId.Id == tradeitem.SenderId).Select(c => new ItemDto
                {
                    Identifier = c.PublicIdentifier,
                    Title = c.Title,
                    ShortDescription = c.ShortDescription,
                    Owner = new UserDto
                    {
                        Identifier = c.OwnerId.PublicIdentifier,
                        FullName = c.OwnerId.FullName,
                        Email = c.OwnerId.Email,
                        ProfileImageUrl = c.OwnerId.ProfileImageUrl
                    }
                }).ToList();
            
            
            var trade = _dbContext.Trades.Include(o => o.Sender).Include(r => r.Receiver)
                .Where(x => x.PublicIdentifier == identifier).Select(x => new TradeDetailsDto
                {
                    Identifier = x.PublicIdentifier,
                    ReceivingItems = recitem,
                    OfferingItems = offeringitem,
                    Receiver = new UserDto
                    {
                        Identifier = x.Receiver.PublicIdentifier,
                        FullName = x.Receiver.FullName,
                        Email = x.Receiver.Email,
                        ProfileImageUrl = x.Receiver.ProfileImageUrl
                    },
                    Sender = new UserDto
                    {
                        Identifier = x.Sender.PublicIdentifier,
                        FullName = x.Sender.FullName,
                        Email = x.Sender.Email,
                        ProfileImageUrl = x.Sender.ProfileImageUrl
                    },
                    ReceivedDate = DateTime.Now,
                    IssuedDate = x.IssuerDate,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy,
                    Status = x.TradeStatus.ToString()
                });
            return trade.First();
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            
            if (user == null)
            {
                throw new ResourceNotFoundException("User not Found :C ");
            }

            if (onlyIncludeActive)
            {
                var trado = _dbContext.Trades.Where(t=> (t.Receiver.Id == user.Id || t.SenderId == user.Id) && t.TradeStatus == TradeStatus.Pending).Select(t=> new TradeDto
                {
                    Identifier = t.PublicIdentifier,
                    IssuedDate = t.IssuerDate,
                    ModifiedDate = t.ModifiedDate,
                    ModifiedBy = t.ModifiedBy,
                    Status = t.TradeStatus.ToString()
                });
                return trado;
            }
            else
            {
                var trado = _dbContext.Trades.Where(t=> t.Receiver.Id == user.Id || t.SenderId == user.Id).Select(t=> new TradeDto
                {
                    Identifier = t.PublicIdentifier,
                    IssuedDate = t.IssuerDate,
                    ModifiedDate = t.ModifiedDate,
                    ModifiedBy = t.ModifiedBy,
                    Status = t.TradeStatus.ToString()
                });
                return trado;
            }


            
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            
            if (user == null)
            {
                throw new ResourceNotFoundException("User not Found :C ");
            }

            var trado = _dbContext.Trades.Where(t=> t.Receiver.Id == user.Id || t.SenderId == user.Id && t.TradeStatus == TradeStatus.Accepted).Select(t=> new TradeDto
            {
                Identifier = t.PublicIdentifier,
                IssuedDate = t.IssuerDate,
                ModifiedDate = t.ModifiedDate,
                ModifiedBy = t.ModifiedBy,
                Status = t.TradeStatus.ToString()
            });
            return trado;
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.PublicIdentifier == userIdentifier);
            if (user == null)
            {
                throw new ResourceNotFoundException("User not Found :C ");
            }
            var trade = _dbContext.Trades.Include(t => t.Sender).Include(r => r.Receiver).Where(t =>
                t.Receiver.PublicIdentifier == userIdentifier || t.Sender.PublicIdentifier == userIdentifier).Select(
                x => new TradeDto
                {
                    Identifier = x.PublicIdentifier,
                    IssuedDate = x.IssuerDate,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy,
                    Status = x.TradeStatus.ToString()
                });
            return trade;
        }

        public TradeDetailsDto UpdateTradeRequest(string email, string identifier, Models.Enums.TradeStatus newStatus)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            var trade = _dbContext.Trades.FirstOrDefault(t => t.PublicIdentifier == identifier);
            if (trade == null)
            {
                throw new ResourceNotFoundException("Trade not Found :C ");
            }
            if (user == null)
            {
                throw new ResourceNotFoundException("User not Found :C ");
            }
            if (trade.TradeStatus != TradeStatus.Pending)
            {
                throw new Exception("Invalid Trade");
            }
            if (user.Id == trade.ReceiverId)
            {
                if (newStatus is TradeStatus.Accepted or TradeStatus.Declined)
                {
                    trade.TradeStatus = newStatus;
                }
                else
                {
                    throw new ArgumentNullException("Invalid TradeStatus :( ");
                }
            }

            if (user.Id == trade.SenderId)
            {
                if (newStatus is TradeStatus.Cancelled)
                {
                    trade.TradeStatus = newStatus;
                }
                else
                {
                    throw new ArgumentNullException("Invalid TradeStatus :( ");
                }
            }
            trade.ModifiedDate = DateTime.Now;
            trade.ModifiedBy = user.FullName;
            _dbContext.SaveChanges();
            return GetTradeByIdentifier(identifier);
        }
    }
}