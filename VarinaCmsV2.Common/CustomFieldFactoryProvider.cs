using System.Collections.Generic;
using VarinaCmsV2.Common.CustomFields;

namespace VarinaCmsV2.Common
{
    public abstract class CustomFieldFactoryProvider<TSaveDataType> where TSaveDataType : class
    {

        /// <typeparam name="TSaveDataType">indicates factories that saves data with this Type</typeparam>
        public abstract List<CustomFieldFactory<TSaveDataType>> GetAvailableFactories();
        public abstract CustomFieldFactory<TSaveDataType> GetFieldFactory(string fieldType);



        //protected List<Type> FindFactoriesFromAssembly(Assembly assembly) 
        //{
        //    return assembly.GetReferencedAssemblies().GetTypes().Where(x => typeof(CustomFieldFactory<TSaveDataType>).IsAssignableFrom(x)).ToList();
        //}

        //protected CustomFieldFactory<TSaveDataType> GetBySavingDataDataType(List<Type> factories, string fieldType) 
        //{
        //    var filtered = factories.Where(x => x.GetGenericArguments().Any(ga => ga is TSaveDataType));
        //    var factoryType = filtered.FirstOrDefault(x =>
        //    {
        //        var customFieldFactory = Activator.CreateInstance(x) as CustomFieldFactory<TSaveDataType>;
        //        return customFieldFactory != null && (customFieldFactory.FieldType == fieldType);
        //    });
        //    return factoryType == null ? null : Activator.CreateInstance(factoryType) as CustomFieldFactory<TSaveDataType>;
        //}
    }
}