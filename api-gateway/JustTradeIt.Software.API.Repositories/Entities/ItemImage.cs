namespace JustTradeIt.Software.API.Repositories.Entities
{
    public class ItemImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int ItemId { get; set; }
        
        //Nav Prop
        public Item ItemNav { get; set; }
        
    }
}