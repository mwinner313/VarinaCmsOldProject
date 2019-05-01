(function () {
    'use strict';

    angular
        .module('app')
        .controller('DiscountAddCtrl', DiscountAddCtrl);

    DiscountAddCtrl.$inject = ['discountSrv', '$mdToast', '$state', 'datetimeRegex', '$rootScope', '$mdDialog', 'lodash'];

    function DiscountAddCtrl(discountSrv, $mdToast, $state, datetimeRegex, $rootScope, $mdDialog, lodash) {
        var vm = this;
        vm.title = "افزودن تخفیف";
        vm.discount = {
            couponCodeType: 5,
            appliedToProducts: [],
            appliedToCategories: [],
        };
        vm.submit = submit;
        vm.chooseProducts = chooseProducts;
        vm.confirmProductDelete = confirmProductDelete;
        vm.datetimeRegex = datetimeRegex;
        vm.isSendingData = false;
        activate();

        ////////////////

        function activate() {}

        function submit() {
            if (vm.discountForm.$invalid) {
                $mdToast.showSimple('ورودی ها را بررسی کنید!');
                return;
            }

            vm.isSendingData = true;
            discountSrv.post(vm.discount).then(function (res) {
                if (res.status == 200) {
                    $mdToast.showSimple('انجام شد');
                    $state.go('discounts', {
                        lang: $rootScope.currentLang,
                        pageNumber: 1
                    });
                }
            });
        }

        function chooseProducts() {
            var productPickerDialogOptions = {
                templateUrl: '/Src/app/eshop/products/productPicker.tpl.html',
                controller: 'productPickerCtrl',
                controllerAs: 'vm',
                fullscreen: true,
                clickOutsideToClose: true
            }
            $mdDialog.show(productPickerDialogOptions).then(function (products) {
                var newOnes = lodash.differenceBy(products, vm.discount.appliedToProducts, function (p) {
                    return p.id
                })
                Array.prototype.push.apply(vm.discount.appliedToProducts, newOnes);
            });
        }



        function confirmProductDelete(product) {
            $mdDialog.show($mdDialog.confirm()
                    .title('از حذف این  گزینه اطمینان دارید ؟')
                    .textContent(product.title).ok('بلی').cancel('خیر'))
                .then(function (res) {
                    vm.discount.appliedToProducts.splice(vm.discount.appliedToProducts.indexOf(product), 1);
                });
        }
    }
})();