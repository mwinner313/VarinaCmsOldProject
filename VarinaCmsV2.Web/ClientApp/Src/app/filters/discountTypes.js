(function() {
    'use strict';

    angular
        .module('app')
        .filter('discountType', discountType);

    function discountType() {
        return discountTypeFilter;

        ////////////////

        function discountTypeFilter(type) {
            switch (type){
                case 1:return "به مجموع کل خرید";
                case 2:return "به محصولات";
                case 5:return "به دسته بندی محصولات";
                case 10:return "هزینه ارسال";
            }
        }
    }
})();