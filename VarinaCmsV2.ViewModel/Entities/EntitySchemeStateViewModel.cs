namespace VarinaCmsV2.ViewModel.Entities
{
    public class EntitySchemeStateViewModel
    {
        public bool CanDelete => EntitiesCount == 0;
        public long EntitiesCount { get; set; }
    }
}