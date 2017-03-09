using System.ComponentModel.DataAnnotations;

namespace asp.net_mvc_event_manager.ViewModels.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}