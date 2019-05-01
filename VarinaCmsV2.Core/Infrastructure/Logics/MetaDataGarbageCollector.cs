//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Text;
//using System.Threading.Tasks;
//using VarinaCmsV2.Core.Contracts.DataServices;
//using VarinaCmsV2.Data.Contracts;
//using VarinaCmsV2.DomainClasses.Contracts;
//using VarinaCmsV2.DomainClasses.Entities;

//namespace VarinaCmsV2.Core.Infrastructure.Logics
//{
//    public class MetaDataGarbageCollector : BaseAfterDeleteEntityLogic
//    {
//        //this code is bullshit have to better logic for Ihavemetadata Items
//        public override async Task ApplyLogicAsync<T>(T obj, IPrincipal requestOwner)
//        {
//            if (obj is IHaveMetaData )
//            {
//                if (obj is FileData)
//                {
//                    var file = obj as FileData;
//                    await DeleteMetas(file.Id, FileData.MetaTypeName);
//                }

//                if (obj is MenuItem)
//                {
//                    var menuItem = obj as MenuItem;
//                    await DeleteMetas(menuItem.Id, MenuItem.MetaTypeName);
//                }
//            }
//        }

//        private async Task DeleteMetas(Guid targetId,string targetType)
//        {
//            var metaDataService = Container.GetInstance<IMetaDataDataService>();
//            var unitOfWork = Container.GetInstance<IUnitOfWork>();

//            metaDataService.Query.Where(x => x.TargetId == targetId && x.TargetType ==targetType )
//                .ToList().ForEach(unitOfWork.Delete);
//            await unitOfWork.SaveChangesAsync();
//        }
//    }
//}
