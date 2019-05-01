(function () {
    'use strict';

    angular
        .module('app')
        .controller('productCategoryWidgetCtrl', productCategoryWidgetCtrl);

    productCategoryWidgetCtrl.$inject = ['timeRange', 'lodash', 'orderOption', '$mdDialog', 'productCategorySrv', 'identityManager', '$q'];

    function productCategoryWidgetCtrl(timeRange, lodash, orderOption, $mdDialog, productCategorySrv, identityManager, $q) {
        var vm = this;
        vm.widget = vm.widget || {
            type: 'productCategory',
            metaData: {
                productCount: 10
            }
        };
        vm.selectedCategory = {
            id: vm.widget.metaData.categoryId
        }
        vm.ok = ok;
        vm.isInRole = identityManager.isInRole;
        vm.closeDialog = closeDialog;
        vm.orderOption = orderOption;
        vm.timeRange = timeRange;
        activate();

        ////////////////

        function activate() {
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            productCategorySrv.get().then(function (res) {
                vm.categories = res.data;
                defered.resolve();
                if (vm.selectedCategory.id) {
                    vm.selectedCategory = lodash.find(res.data, function (cat) {
                        return cat.id == vm.selectedCategory.id
                    });
                }
            });
        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        function ok() {
            vm.widget.metaData.categoryId = vm.selectedCategory.id;
            vm.widget.title = ['', null, undefined].indexOf(vm.widget.title) === -1 ? vm.widget.title : 'دسته بندی' + ' | ' + vm.selectedCategory.title;
            vm.widget.handle = vm.widget.handle || vm.selectedCategory.schemeHandle + vm.selectedCategory.handle;
            $mdDialog.hide(vm.widget);
        }
    }
})();