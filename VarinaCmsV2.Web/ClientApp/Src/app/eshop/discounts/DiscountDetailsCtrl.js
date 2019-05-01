(function () {
    'use strict';

    angular
        .module('app')
        .controller('DiscountDetailCtrl', DiscountDetailCtrl);

    DiscountDetailCtrl.$inject = ['lodash', 'discount', 'discountSrv', 'datetimeRegex', '$mdToast', '$state', '$rootScope', '$mdDialog'];

    function DiscountDetailCtrl(lodash, discount, discountSrv, datetimeRegex, $mdToast, $state, $rootScope, $mdDialog) {

        var vm = this;
        vm.title = "ویرایش تخفیف";
        vm.discount = discount.data;
        vm.submit = submit;
        vm.datetimeRegex = datetimeRegex;
        vm.isSendingData = false;
        vm.chooseProducts = chooseProducts;
        vm.confirmProductDelete = confirmProductDelete;

        function submit() {
            if (vm.discountForm.$invalid) {
                $mdToast.showSimple('مقادیر را بررسی کنید');
                return;
            }
            vm.isSendingData = true;
            discountSrv.put(vm.discount).then(function (res) {
                $mdToast.showSimple('انجام شد');
                $state.go('discounts', {
                    lang: $rootScope.currentLang,
                    pageNumber: 1
                })
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