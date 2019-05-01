using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Tests.EntitiesTests
{
    [TestClass]
    public class ObservableCollectionTests
    {
        [TestMethod]
        public void ShouldGetSelfObjectInHandler()
        {
            var o=new ObservableCollection<Premission>();
            o.CollectionChanged += ReadSender;
            o.Add(new Premission() {Action = "test"});
        }

        private void ReadSender(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = sender as ObservableCollection<Premission>;
            if (collection != null)
            {
               Console.WriteLine(collection.Count);
            }
           
        }
    }
}
