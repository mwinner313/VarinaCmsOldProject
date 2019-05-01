using System.ComponentModel.DataAnnotations;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class LocalizedNameViewModel:BaseVeiwModel
    {
        [Required]
        public string LanguageName { get; set; }
        [Required]
        public string Name { get; set; }

    }
}