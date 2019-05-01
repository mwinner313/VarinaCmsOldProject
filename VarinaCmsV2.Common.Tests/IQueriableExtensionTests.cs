//using System;
//using System.Collections.Generic;
//using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using VarinaCmsV2.DomainClasses.Entities;

//namespace VarinaCmsV2.Common.Tests
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void ShouldSelectDeepIds()
//        {
//            var list = new List<MenuItem>()
//            {
//                new MenuItem()
//                {
//                    Id = Guid.NewGuid(),
//                    Childs = new List<MenuItem>()
//                    {
//                        new MenuItem()
//                        {
//                            Id = Guid.NewGuid(),
//                            Childs = new List<MenuItem>()
//                            {
//                                new MenuItem()
//                                {
//                                    Id = Guid.NewGuid(),
//                                    Childs = new List<MenuItem>()
//                                    {
//                                        new MenuItem()
//                                        {
//                                            Id = Guid.NewGuid(),
//                                        }
//                                    }
//                                }
//                            }
//                        },
//                        new MenuItem()
//                        {
//                            Id = Guid.NewGuid(),
//                            Childs = new List<MenuItem>()
//                            {
//                                new MenuItem()
//                                {
//                                    Id = Guid.NewGuid(),
//                                    Childs = new List<MenuItem>()
//                                    {
//                                        new MenuItem()
//                                        {
//                                            Id = Guid.NewGuid(),
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            };



//         var ids=   list.SelectDeep(x => x.Id);
//            ids.ForEach(x=>Console.WriteLine(x));
//        }
//    }
//}
