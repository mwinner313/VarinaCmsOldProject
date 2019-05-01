using System;
using DotLiquid;

namespace VarinaCmsV2.Common.CustomFields
{
    public delegate bool IsValidForField<T>(T data) where T : class;
    public abstract class CustomField<TSavingData>: ILiquidizable where TSavingData : class
    {
        public readonly string TypeName;
        public readonly string FieldName;
        public TSavingData Value;



        protected CustomField(CustomFieldFactory<TSavingData> factory, string fieldName, TSavingData value):this(factory,fieldName)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        protected CustomField(CustomFieldFactory<TSavingData> factory, string fieldName)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            TypeName = factory.FieldType;
            FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
        }
        public new abstract string ToString();
        public abstract object ToLiquid();
        public abstract string Veiw();
    }
}
