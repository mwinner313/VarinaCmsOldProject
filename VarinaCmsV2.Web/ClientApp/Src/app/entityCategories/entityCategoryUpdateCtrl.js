(function () {
    'use strict';

    angular
        .module('app')
        .controller('EntityCategoryUpdateCtrl', EntityCategoryUpdateCtrl);

    EntityCategoryUpdateCtrl.$inject = ['categoryDataService', '$mdDialog', '$mdToast', 'categoryType','identityManager'];
    function EntityCategoryUpdateCtrl(categoryDataService, $mdDialog, $mdToast, categoryType,identityManager) {

        var vm = this;
        vm.ok = ok;
        vm.close = close;
        vm.categoryType = categoryType;
        vm.isInRole=identityManager.isInRole;

        activate();

        ////////////////

        function activate() { }

        function close() {
            $mdDialog.cancel();
        }

        function ok() {
            vm.categoryForm.$submitted = true;
            if (vm.categoryForm.$invalid) return;
            vm.isSendingData = true;
            categoryDataService.put(vm.category).then(function (res) {
                $mdDialog.hide(vm.category);
                $mdToast.showSimple('انجام شد');
            });
        }
    }
})();