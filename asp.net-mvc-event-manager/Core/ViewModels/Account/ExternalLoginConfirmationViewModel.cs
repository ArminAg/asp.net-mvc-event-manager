﻿using System.ComponentModel.DataAnnotations;

namespace asp.net_mvc_event_manager.Core.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}