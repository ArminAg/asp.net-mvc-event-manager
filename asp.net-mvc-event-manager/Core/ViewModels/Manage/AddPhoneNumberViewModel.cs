﻿using System.ComponentModel.DataAnnotations;

namespace asp.net_mvc_event_manager.Core.ViewModels.Manage
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}