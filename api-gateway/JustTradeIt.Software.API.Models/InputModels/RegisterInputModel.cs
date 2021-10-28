using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(3)]
        public string FullName { get; set; } 
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }
}