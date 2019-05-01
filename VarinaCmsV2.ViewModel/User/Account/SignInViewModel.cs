using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaeinaCmsV2.Resources;
using VarinaCmsV2.Common.MVC;

namespace VarinaCmsV2.ViewModel.User.Account
{
    public class SignInViewModel
    {
        [Required]
        [EmailOrPhone]
        [Display(ResourceType = typeof(UserTexts), Name = nameof(UserTexts.EmailOrPhone))]
        public string EmailOrPhone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(UserTexts), Name = nameof(UserTexts.Passwrod))]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
