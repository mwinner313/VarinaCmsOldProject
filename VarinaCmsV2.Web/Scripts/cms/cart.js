$(document).ready(function () {

    $('[add-to-cart]').click(function (e) {
        e.preventDefault();
    });

    $('[remove-from-cart]').click(function (e) {
        e.preventDefault();
    });

    $('[add-to-cart]').click(addToCart);
    $('[remove-from-cart]').click(removeFromCart);

   // $('[update-shopping-cart]').click(updateShoopingCart);


    function addToCart(e) {
        e.preventDefault();
        var $el = $(this);
        $el.off("click", addToCart);

        var data = {
            productId: $el.attr('product-id'),
            quantity: $('[name="quantity"]').val() || 1,
            __RequestVerificationToken: $('[name="__RequestVerificationToken"').val()
        }

        $.ajax({
            type: "POST",
            url: "/Cart/Add",
            data: data,
            success: function (response) {
                $el.click(addToCart);
                $.toast({
                    text: response.Message,
                    afterHidden: function () {
                        //   location.reload();
                    }
                });
            }
        });
    }

    function removeFromCart(e) {
        e.preventDefault();
        var $el = $(this);
        $el.off("click", removeFromCart);

        var data = {
            productId: $el.attr('product-id'),
            __RequestVerificationToken: $('[name="__RequestVerificationToken"').val()
        }

        $.ajax({
            type: "POST",
            url: "/Cart/Remove",
            data: data,
            success: function (response) {
                $el.click(removeFromCart);
                $.toast({
                    text: response.Message,
                    afterHidden: function () {
                        // location.reload();
                    }
                });
            }
        });
    }

    function updateShoopingCart(e) {
        e.preventDefault();
        var data = getCartData();
        $.ajax({
            type: "Put",
            url: "/Cart/UpdateShoppingItems",
            data: {
                shoppingCartItems: data,
                proceedWithOrder: false
            },
            success: function (response) {
                $.toast({
                    text: response.Message,
                    afterHidden: function () {

                    }
                });
            }
        });
    }

    function getCartData() {
        var $cart = $('[shopping-cart]');
        var data = [];

        $cart.find('[shopping-cart-item]').each(function (e) {
            data.push({
                id: $(this).find('name="id"').val(),
                quantity: $(this).find('name="quantity"').val()
            })
        });
        return data;
    }
})