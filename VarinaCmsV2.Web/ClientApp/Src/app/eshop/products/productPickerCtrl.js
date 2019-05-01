(function () {
    'use strict';

    angular
        .module('app')
        .controller('productPickerCtrl', productPickerCtrl);

    productPickerCtrl.$inject = ['$mdDialog', 'productSrv', '$q'];

    function productPickerCtrl($mdDialog, productSrv, $q) {
        var vm = this;
        vm.query = {
            pageSize: 30,
            pageNumber: 1
        }
        vm.getProducts = getProducts;
        vm.selectedProducts = [];
        vm.select=select;

        activate();

        ////////////////

        function activate() {
            getProducts(1,30)
        }
        function select(){
            $mdDialog.hide(vm.selectedProducts)
        }
        
        function getProducts(pageNumber, pageSize) {
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            productSrv.get({
                title: vm.searchText,
                pageSize: pageSize,
                pageNumber: pageNumber
            }).then(function (res) {
                defered.resolve(res.data);
                vm.products = res.data;
            });
        }
    }
})();