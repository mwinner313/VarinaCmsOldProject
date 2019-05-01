//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using VarinaCmsV2.Core.Infrastructure;

//namespace VarinaCmsV2.IocConfig.Tests
//{
//    [TestClass]
//    public class InfrustuctureFindingTests
//    {
//        [TestMethod]
//        public void FindLogics()
//        {
//            var list = new List<IInfrustructureLogic>();
//            var infraLogicType = typeof(IInfrustructureLogic);
//            var concereteLogicsTypes = infraLogicType.Assembly.GetTypes().Where(x => infraLogicType.IsAssignableFrom(x) && x.IsClass).ToList();
//            foreach (var logicType in concereteLogicsTypes)
//            {
//                Console.WriteLine(logicType);
//                if (logicType.IsGenericType)
//                {
//                    var genericTypeDefinition = logicType.GetGenericTypeDefinition();
//                    var objects = infraLogicType.Assembly.GetTypes().ToList();

//                    objects = FilterByTypeArgument(genericTypeDefinition, logicType, objects);
//                    foreach (var type in objects)
//                    {
//                        Console.WriteLine(type);
//                    }
//                }

//            }
//        }

//        private static List<Type> FilterByTypeArgument(Type genericTypeDefinition, Type logicType, List<Type> objects)
//        {
//            foreach (var genericTypeArgument in genericTypeDefinition.GetGenericArguments())
//            {
//                objects = FilterByTypeConstraints(objects, genericTypeArgument);
//            }
//            return objects;
//        }

//        private static List<Type> FilterByTypeConstraints(List<Type> objects, Type genericTypeArgument)
//        {
//            foreach (var constraint in genericTypeArgument.GetGenericParameterConstraints())
//            {
//                if (constraint.GenericTypeArguments.Length != 0)
//                {
//                    foreach (var argument in constraint.GetGenericArguments())
//                    {
//                        var cs = argument.GetGenericParameterConstraints();
//                        if (cs.Length == 1 && cs[0]==(constraint))
//                            objects = objects.Where(x => constraint.IsAssignableFrom(x)).ToList();
//                        else
//                            objects = FilterByTypeConstraints(objects, argument);
//                    }
//                }
//                else
//                {
//                    objects = objects.Where(x => constraint.IsAssignableFrom(x)).ToList();
//                }
//            }
//            return objects;
//        }
//    }
//}
