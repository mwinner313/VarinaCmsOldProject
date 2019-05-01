using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using DotLiquid;
using StructureMap;
using VarinaCmsV2.Core.Contracts.Decorators;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.FrontEndOptions;
using VarinaCmsV2.Core.Logic.Widgets;
using VarinaCmsV2.Core.Logic.Helpers;

namespace VarinaCmsV2.Core.Logic.Decorators
{
    public class RelatedEntityLoaderForEntityDecorator : LiquidDataDecorator
    {
        private readonly Drop _wrapee;
        private IEntityDataService _entities;
        public const string MethodHandle = "related_entities_";
        public RelatedEntityLoaderForEntityDecorator(Drop wrapee)
        {
            _wrapee = wrapee;
        }
        public RelatedEntityLoaderForEntityDecorator()
        {

        }
        public override int LevelToReachRealWrappe => 1;

        public override bool ShouldDecorate(Drop wrappe)
        {
            return wrappe is EntityLiquidAdapter;
        }

        public override LiquidAdapter Decorate(Drop wrapee)
        {
            return new RelatedEntityLoaderForEntityDecorator(wrapee) { Container = Container };
        }

        public override object BeforeMethod(string method)
        {
            if (method.StartsWith(MethodHandle))
            {
                if (!(_wrapee is EntityLiquidAdapter entity)) return _wrapee.InvokeDrop(method);

                _entities = Container.GetInstance<IEntityDataService>();

                var options = GetLoadOptions(method);

                return _entities.Query.IncludeNecessaryData()
                    .Where(x => x.PrimaryCategoryId == entity.Entity.PrimaryCategoryId && x.Id != entity.Id)
                    .GetWithAppliedOrder(options.Order)
                    .GetWithApliedTimeRange(options.TimeRange)
                    .Take(options.Count).ToList()
                    .AsLiquidAdapted();
            }

            return _wrapee.InvokeDrop(method);
        }

        private RelatedEntityLoadOption GetLoadOptions(string method)
        {
            var opt = FrontEndDeveloperOptions.Instance.EntityRelatedItemsLoadOptions.FirstOrDefault(x => x.Handle == method.Replace(MethodHandle, ""));
            if (opt == null) throw new InvalidOperationException("no entity_related option exists in front end config");
            return opt;
        }
    }
}
