using System.Data.Entity;
using System.Linq;
using System.Web;
using DotLiquid;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Decorators;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Logic.Decorators
{
    public class CurrentUserLiquidDataDecorator : LiquidDataDecorator
    {
        private readonly Drop _wrappe;

        public CurrentUserLiquidDataDecorator()
        {
        }

        public CurrentUserLiquidDataDecorator(Drop wrappe)
        {
            _wrappe = wrappe;
        }

        public override int LevelToReachRealWrappe { get; } = 5;

        public override bool ShouldDecorate(Drop wrappe)
        {
            return true;
        }

        public override LiquidAdapter Decorate(Drop wrapee)
        {
            return new CurrentUserLiquidDataDecorator(wrapee) {Container = Container};
        }

        public override object BeforeMethod(string method)
        {
            if (method == "current_user") return HandleMethod();
            return _wrappe != null ? _wrappe.BeforeMethod(method) : base.BeforeMethod(method);
        }

        private object HandleMethod()
        {
            var users = Container.GetInstance<IUnitOfWork>().Set<User>().Include(x => x.ShoppingCartItems)
                .Include(x => x.ShoppingCartItems.Select(p => p.Product));
            var currUserId = HttpContext.Current.User.Identity.GetUserId().ToGuid();
            var user = users.FirstOrDefault(x => x.Id == currUserId);
            return user?.AsLiquidAdapted();
        }
    }
}