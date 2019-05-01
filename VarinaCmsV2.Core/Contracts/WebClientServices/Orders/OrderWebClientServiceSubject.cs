using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.WebClientServices.Comment;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Orders
{
    public class OrderWebClientServiceSubject : IOrderWebClientService
    {
        private readonly List<IOrderWebClientObserver> _observers = new List<IOrderWebClientObserver>();
        private IOrderWebClientService _wrappe;
        public void SetWrappe(IOrderWebClientService wrappe)
        {
            _wrappe = wrappe;
        }
        public void AddObserver(IOrderWebClientObserver observer)
        {
          _observers.Add(observer);
        }
        public async Task<Order> PlaceCurrentUserCartAsync()
        {
            var res = await _wrappe.PlaceCurrentUserCartAsync();
            try
            {
                await _observers.ForEachAsync(x => x.OrderPlaced(res));
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
            }
            return res;
        }

        public async Task<Order> PostProcessOrderPaymentAsync(Guid orderId)
        {
            var res = await _wrappe.PostProcessOrderPaymentAsync(orderId);
            try
            {
                await _observers.ForEachAsync(x => x.OrderPaid(res));
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
            }
            return res;
        }

        public Task CountUpDownloadCountForProductOrderItem(Guid orderId, Guid productId)
        {
            return _wrappe.CountUpDownloadCountForProductOrderItem(orderId, productId);
        }
    }
}
