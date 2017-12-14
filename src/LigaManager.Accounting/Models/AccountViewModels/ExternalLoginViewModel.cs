using System.ComponentModel.DataAnnotations;

namespace LigaManager.Accounting.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
