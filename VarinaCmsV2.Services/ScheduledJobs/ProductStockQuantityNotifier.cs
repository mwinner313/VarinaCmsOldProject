using System.ComponentModel.DataAnnotations;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Services.ScheduledJobs
{
    public class ProductStockQuantityNotifier : InjectibleJob
    {
        private readonly IEmailyService _emailyService;
        private readonly Product _product;
        private readonly string _emailAddress;
        public ProductStockQuantityNotifier(Product product, string emailAddress)
        {
            _product = product;
            _emailAddress = emailAddress;
            _emailyService = Container.GetInstance<IEmailyService>();
        }

        public override void Execute()
        {

            if (string.IsNullOrEmpty(_emailAddress) || !new EmailAddressAttribute().IsValid(_emailAddress))
            {
                TelegramMessenger.SendMessage("empty email address to send product inventory info");
                return;
            }

            AsyncHelper.RunSync(() => _emailyService.SendAsync(new EmailContext()
            {
                Content =
                    $"موجودی انبار محصول {_product.Title} به تعداد {_product.Inventory.StockQuantity} رسید. ",
                To = _emailAddress,
                Subject = "اطلاع رسانی وب سایت من => موجودی محصول ",
            }));
        }
    }
}
