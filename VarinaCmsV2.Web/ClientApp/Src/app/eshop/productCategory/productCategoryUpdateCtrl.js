(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductCategoryUpdateCtrl', ProductCategoryUpdateCtrl);

    ProductCategoryUpdateCtrl.$inject = ['productCategorySrv', '$mdDialog', '$mdToast', 'categoryType', 'identityManager'];

    function ProductCategoryUpdateCtrl(productCategorySrv, $mdDialog, $mdToast, categoryType, identityManager) {

        var vm = this;
        vm.ok = ok;
        vm.close = close;
        vm.categoryType = categoryType;
        vm.isInRole = identityManager.isInRole;

        activate();

        ////////////////

        function activate() {}

        function close() {
            $mdDialog.cancel();
        }

        function ok() {
            vm.categoryForm.$submitted = true;
            if (vm.categoryForm.$invalid) return;
            vm.isSendingData = true;
            productCategorySrv.put(vm.category).then(function (res) {
                $mdDialog.hide(vm.category);
                $mdToast.showSimple('انجام شد');
            });
        }
    }
})();