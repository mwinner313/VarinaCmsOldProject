using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Services.Helpers;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class ShoppingCartHelper : IShoppingCartHelper
    {
        public string GetAddToCartValidationErrors(User user, Product product, int quantity, IRestrictedItemAccessManager accessManager, IUnitOfWork unitOfWork)
        {
            var err = string.Empty;

            if (!accessManager.HasAccess(product, AccessPremission.See))
                err = "شما اجازه دسترسی به این محصول را ندارید";

            if (product.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity &&
                product.Inventory.StockQuantity == 0)
                err = "موجودی محصول پایان یافته";

            if (product.Inventory.StockQuantity - product.Inventory.ReservedQuantity == 0)
                err = "موجودی محصول پایان یافته کاربر دیگری در حال خرید این محصول میباشد تا لحظاتی دیگری تلاش کنید در صورت عدم نهایی شدن پرداخت آن کاربر شما میتوانید این محصول را خریداری کنید.";

            var existingShoppingCartItem = user.ShoppingCartItems.FirstOrDefault(x => x.Product.Id == product.Id);
            if (existingShoppingCartItem != null &&
                existingShoppingCartItem.Quantity + quantity > (product.Inventory.StockQuantity - product.Inventory.ReservedQuantity) &&
                product.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity)
                err = "موجودی محصول کم تر از تعداد درخواست شما در سبد خرید میباشد";


            if (!string.IsNullOrEmpty(product.Inventory.AllowedQuantities))
            {
                var allowedOpts = product.Inventory.AllowedQuantities.ParseNumbers();
                if (!allowedOpts.Contains(quantity)) err = "! امکان خرید با این تعداد  مجاز نمیباشد";
            }

            if (quantity > product.Inventory.OrderMaximumQuantity || quantity < product.Inventory.OrderMinimumQuantity)
                err =
                    $"امکان خرید با این تعداد مجاز نمیباشد تعداد مجاز سبد خرید از{product.Inventory.OrderMinimumQuantity} تا {product.Inventory.OrderMaximumQuantity} عدد میباشد";


            if (!product.CanAddToCart)
                err = "امکان افزودن این محصول به سبد خرید وجود ندارد";

            if (product.Type == ProductType.Grouped)
                err = "امکان افزودن محصول گروهی به سبد خرید موجود نمیباشد";



            if (product.HasRequiredProducts)
            {
                bool doesUserBuiedRequiredProducts =
                    DoesUserBuyThisProducts(product.RequiredProducts, user, unitOfWork);
                if (!product.AutomaticallyAddRequiredProducts)
                    err =
                        $"خرید این محصول نیاز به خرید محصول {string.Join(" ,", product.RequiredProducts.Select(x => x.Title))} دارد.";
                if (product.IsRequiredProductOutOfStock() && !doesUserBuiedRequiredProducts)
                    err = "محصولات الزامی مربوط به این محصول ناموجد است";
            }


            return err;
        }



        public async Task AddToCartAsync(Product product, int quantity, User user, IUnitOfWork unitOfWork)
        {
            if (user.ShoppingCartItems.Any(x => x.Product.Id == product.Id))
            {
                var exitingItem = user.ShoppingCartItems.First(x => x.Product.Id == product.Id);
                exitingItem.Quantity += quantity;
                exitingItem.UpdateDateTime = DateTime.Now;
                unitOfWork.Update(exitingItem);
            }
            else
            {
                user.ShoppingCartItems.Add(new ShoppingCartItem
                {
                    CreateDateTime = DateTime.Now,
                    ProductId = product.Id,
                    Quantity = quantity,
                    UpdateDateTime = DateTime.Now
                });
            }

            HandleRequiredProducts(product, user, unitOfWork);

            await unitOfWork.SaveChangesAsync();
        }

        public string GetRemoveFromCartValidationErrors(User user, Product product)
        {
            var existingItem = user.ShoppingCartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (existingItem is null) return "چنین محصولی در سبد خرید وجود ندارد";

            if (user.ShoppingCartItems.Any(x => x.Product.RequiredProducts.Contains(existingItem.Product)))
            {
                var pendTo =
                    user.ShoppingCartItems.First(x => x.Product.RequiredProducts.Contains(existingItem.Product));
                return $"این محصول به الزام {pendTo.Product.Title} اضافه شده است.";
            }
            return null;
        }

        public async Task RemoveFromCart(Product product, User user, IUnitOfWork unitOfWork)
        {
            var updatingShoppingCartItem = user.ShoppingCartItems.First(x => x.ProductId == product.Id);
            unitOfWork.Delete(updatingShoppingCartItem);
            await unitOfWork.SaveChangesAsync();
        }

        public void HandleUserCartWarnings(User user)
        {
            foreach (var item in user.ShoppingCartItems)
            {
                if (item.Product.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity &&
                    item.Product.Inventory.StockQuantity == 0)
                    item.Warnings.Add("موجودی محصول پایان یافته");

                if (item.Product.Inventory.StockQuantity - item.Product.Inventory.ReservedQuantity == 0)
                    item.Warnings.Add(
                        "موجودی محصول پایان یافته کاربر دیگری در حال خرید این محصول میباشد تا لحظاتی دیگری تلاش کنید در صورت عدم نهایی شدن پرداخت آن کاربر شما میتوانید این محصول را خریداری کنید.");
                

                if (item.Quantity > item.Product.Inventory.OrderMaximumQuantity ||
                    item.Quantity < item.Product.Inventory.OrderMinimumQuantity)
                    item.Warnings.Add(
                        $"امکان خرید با این تعداد مجاز نمیباشد تعداد مجاز سبد خرید از{item.Product.Inventory.OrderMinimumQuantity} تا {item.Product.Inventory.OrderMaximumQuantity} عدد میباشد");

                if (!string.IsNullOrEmpty(item.Product.Inventory.AllowedQuantities))
                {
                    var allowedOpts = item.Product.Inventory.AllowedQuantities.ParseNumbers();
                    if (!allowedOpts.Contains(item.Quantity))
                        item.Warnings.Add("! امکان خرید با این تعداد  مجاز نمیباشد");
                }
                if (
                    item.Quantity > (item.Product.Inventory.StockQuantity - item.Product.Inventory.ReservedQuantity) &&
                    item.Product.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity ||
                    item.Quantity < 1)
                    item.Warnings.Add("موجودی محصول کم تر از تعداد درخواست شما در سبد خرید میباشد");
                if (!item.Product.CanAddToCart)
                    item.Warnings.Add("امکان خرید این محصول در حال حاضر امکان پذیر نمیباشد");


            }
        }

        private void HandleRequiredProducts(Product product, User user, IUnitOfWork unitOfWork)
        {
            if (product.HasRequiredProducts && product.AutomaticallyAddRequiredProducts)
                product.RequiredProducts.ToList().ForEach(x =>
                {
                    if (!HasUserInShoppingCart(x, user) ||
                        !DoesUserBuyThisProduct(x, user, unitOfWork))
                        user.ShoppingCartItems.Add(new ShoppingCartItem
                        {
                            ProductId = x.Id,
                            CreateDateTime = DateTime.Now,
                            Quantity = product.GetMinimumCartQuantity(),
                            UpdateDateTime = DateTime.Now
                        });
                });
        }
        private bool HasUserInShoppingCart(Product product, User user)
        {
            return user.ShoppingCartItems.Select(p => p.Product).Contains(product);
        }
        private bool DoesUserBuyThisProduct(Product product, User user, IUnitOfWork unitOfWork)
        {
            return !unitOfWork.Set<Order>().Any(o =>
                 o.CreatorId == user.Id && o.OrderItems.Any(i => i.ProductId == product.Id) &&
                 o.OrderStatus == OrderStatus.Complete);
        }
        private bool DoesUserBuyThisProducts(ICollection<Product> products, User user, IUnitOfWork unitOfWork)
        {
            var does = true;
            foreach (var product in products)
            {
                if (!DoesUserBuyThisProduct(product, user, unitOfWork))
                    does = false;
            }
            return does;
        }
    }
}