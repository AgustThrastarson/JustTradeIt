using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ItemInputModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string ConditionCode { get; set; } //Vantar að gera validaiton
        public IEnumerable<string> ItemImages { get; set; }
    }
}