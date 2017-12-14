using System.ComponentModel.DataAnnotations;

namespace LigaManager.Accounting.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
