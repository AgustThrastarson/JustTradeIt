using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;


namespace JustTradeIt.Software.API.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Envelope<ItemDto> GetAllItems(int pageSize, int pageNumber, bool ascendingSortOrder);
        ItemDetailsDto GetItemByIdentifier(string identifier);
        string AddNewItem(string email, ItemInputModel item);
        void RemoveItem(string email, string identifier);
    }
}