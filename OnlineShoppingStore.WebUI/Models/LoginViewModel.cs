using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Passsword is required")]
        [StringLength(maximumLength:50, MinimumLength=6)]
        public string Password { get; set; }
    }
}