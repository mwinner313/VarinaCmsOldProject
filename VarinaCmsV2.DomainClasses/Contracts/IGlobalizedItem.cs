using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IGlobalizedItem
    {
         string LanguageName { get; set; }
         Language Language { get; set; }
    }
}
