using System;
using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Entities;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly TradeItDbContext _dbContext;

        public ItemRepository(TradeItDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public string AddNewItem(string email, ItemInputModel item)
        {
            ICollection<ItemImage> images = new List<ItemImage>();
            
            
            var itemcond = _dbContext.ItemConditions.FirstOrDefault(x => x.ConditionCode == item.ConditionCode);
            if (itemcond == null)
            {
                
                itemcond = new ItemCondition
                {
                    ConditionCode = item.ConditionCode,
                    Description = null
                };
                _dbContext.ItemConditions.Add(itemcond);
                _dbContext.SaveChanges();
            }
            
            var user = _dbContext.Users.First(x => x.Email == email);
            
            var entity = new Item
            {
                PublicIdentifier = Guid.NewGuid().ToString(),
                Title = item.Title,
                ShortDescription = item.ShortDescription,
                Description = item.Description,
                ItemImages = images,
                ItemConditionId = itemcond.Id,
                OwnerId = user,
                Deleted = false
            }; 
                foreach (var image in item.ItemImages)
                {
                    images.Add(new ItemImage
                    {
                        ImageUrl = image,
                        ItemNav = entity,
                        ItemId = entity.Id,
                    });
                }
            _dbContext.Items.Add(entity);
            _dbContext.SaveChanges();
            return entity.PublicIdentifier;
        }
        
        

        public Envelope<ItemDto> GetAllItems(int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            var items = _dbContext.Items.Include(x => x.OwnerId).Where(t => t.Deleted != true).Select(x => new ItemDto
            {
               Identifier = x.PublicIdentifier,
               Title = x.Title,
               ShortDescription = x.ShortDescription,
               Owner = new UserDto
               {
                   Identifier = x.OwnerId.PublicIdentifier,
                   FullName = x.OwnerId.FullName,
                   Email = x.OwnerId.Email,
                   ProfileImageUrl = x.OwnerId.ProfileImageUrl
               }
               
            });
            
            if (!ascendingSortOrder)
            {
                items.OrderByDescending(x => x.Title);
            }
            else
            {
                items.OrderBy(x => x.Title);
            }


            return new Envelope<ItemDto>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = items
            };
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            var itemid = _dbContext.Items.FirstOrDefault(x => x.PublicIdentifier == identifier).Id;
            var NrOfActiveTrades = _dbContext.TradeItems.Where(x=> x.ItemId == itemid).Select(x=> x.Trade).Count(l => l.TradeStatus == TradeStatus.Pending);
            var detaileditem = _dbContext.Items.Include(i => i.ItemImages).Where(x => x.PublicIdentifier == identifier)
                .Select(x => new ItemDetailsDto
                {
                    Identifier = x.PublicIdentifier,
                    Title = x.Title,
                    Description = x.Description,
                    Images = x.ItemImages.Select(i => new ImageDto
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl
                    }),
                    NumberOfActiveTradeRequests = NrOfActiveTrades,
                    Condition = _dbContext.ItemConditions.First(z => z.Id == x.ItemConditionId).ConditionCode,
                    Owner = x.OwnerId.FullName
                });
            return detaileditem.FirstOrDefault();

        }
        
        public void RemoveItem(string email, string identifier)
        {
            var removeitem = _dbContext.Items.Include(x => x.OwnerId).Include(t => t.TradeItems)
                .ThenInclude(k => k.Trade).FirstOrDefault(x => x.OwnerId.Email == email && x.PublicIdentifier == identifier && x.Deleted == false);
            if (removeitem != null)
            {
                removeitem.Deleted = true;
                _dbContext.SaveChanges();

            }
            else
            {
                //TODO kasta villu 
                throw new Exception("Item not found");
            }
            
            var tradeitems = removeitem.TradeItems.Select(t => t.Trade);
            foreach (var trades in tradeitems)
            {
                trades.TradeStatus = TradeStatus.Cancelled;
            }
            
            
            
            
        }
    }
}