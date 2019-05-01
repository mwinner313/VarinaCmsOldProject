using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.WebClientServiceObservers
{
    public class EntityVisitNumberobserver : EntityWebClientServiceObserver
    {
        private readonly IUnitOfWork _unitOfwork;
        public EntityVisitNumberobserver(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }
        public override async Task EntitySeenByUser(Entity entity)
        {
            var entitySeenCookie = GenerateCookie(entity);
            if (HttpContext.Current.Request.Cookies.AllKeys.ToList().All(x => x != entitySeenCookie.Name))
            {
                HttpContext.Current.Response.Cookies.Add(entitySeenCookie);
                entity.VisitCount++;
                _unitOfwork.Update(entity);
                await _unitOfwork.SaveChangesAsync();
            }
        }

        private HttpCookie GenerateCookie(Entity entity)
        {
            return new HttpCookie($"Entity-Seen-{entity.Id}") {Expires = DateTime.Now.AddDays(7)};
        }
    }
}
