namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface ISoftDeletibleItem
    {
         bool IsDeleted { get; set; }
    }
}
