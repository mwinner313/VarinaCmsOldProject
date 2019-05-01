using System;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel
{
    public class TagViewModel: IUrlableViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? EntitySchemeId { get; set; }
        public string LanguageName { get; set; }
        public string ToUrl { get; set; }
        public string ToFullUrl { get; set; }
    }
}