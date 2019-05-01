(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductCategoryAddCtrl', ProductCategoryAddCtrl);

    ProductCategoryAddCtrl.$inject = ['$mdDialog', 'productCategorySrv', 'currentLang', '$mdToast', 'categoryType', 'identityManager'];

    function ProductCategoryAddCtrl($mdDialog, productCategorySrv, currentLang, $mdToast, categoryType, identityManager) {
        var vm = this;
        vm.category = {
            languageName: currentLang.get(),
            fieldDefenitionGroups:[],
            fieldDefenitions:[],
            categoryType: categoryType.mixed
        }
        vm.close = close;
        vm.newFdGroup = {
            fieldDefenitions: [],
           
        };
        vm.categoryType = categoryType;
        vm.ok = ok;
        vm.isInRole = identityManager.isInRole;
        activate();
        ////////////////
       
        function activate() {}

        function close() {
            $mdDialog.hide();
        }

        function ok() {
            vm.categoryForm.$submitted = true;
            if (vm.categoryForm.$invalid) return;
            vm.isSendingData = true;
            productCategorySrv.post(vm.category).then(function (res) {
                vm.isSendingData = false;
                if (res.status !== 200) return;
                $mdDialog.hide(res.data);
                $mdToast.showSimple('انجام شد');
            });
        }

    }
})();