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
    public class SignUpViewModel
    {
        [Required]
        [EmailOrPhone]
        [Display(ResourceType = typeof(UserTexts),Name = nameof(UserTexts.EmailOrPhone))]
        public string EmailOrPhone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "رمز عبور میبایست بیشتر 6 حرف باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(UserTexts), Name = nameof(UserTexts.Passwrod))]
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessageResourceType = typeof(UserTexts),ErrorMessageResourceName = nameof(UserTexts.ConfirmPasswordNotMatched))]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
