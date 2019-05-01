using DotLiquid;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
    [LiquidType("Value","Name")]
    public class DropDownListFieldTemplate 
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }


}
