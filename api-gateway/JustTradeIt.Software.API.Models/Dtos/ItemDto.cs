namespace JustTradeIt.Software.API.Models.Dtos
{
    public class ItemDto
    {
        public string Identifier { get; set; }
        
        public string Title { get; set; }
        
        public string ShortDescription { get; set; }
        
        public UserDto Owner { get; set; }
    }
}