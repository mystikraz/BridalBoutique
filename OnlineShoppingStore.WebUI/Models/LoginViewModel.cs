﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}