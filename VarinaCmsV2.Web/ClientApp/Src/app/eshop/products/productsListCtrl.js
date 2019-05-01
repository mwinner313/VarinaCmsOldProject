(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductsListCtrl', ProductsListCtrl);

    ProductsListCtrl.$inject = ['$rootScope', '$stateParams', '$state', 'productSrv', '$location', 'products', '$q', 'categories', 'bottomSheetOptions', '$mdBottomSheet', '$mdDialog', '$mdToast'];

    function ProductsListCtrl($rootScope, $stateParams, $state, productSrv, $location, products, $q, categories, bottomSheetOptions, $mdBottomSheet, $mdDialog, $mdToast) {
        var vm = this;
        vm.selectedTags = [];
        vm.selectedCat;
        vm.removeCatIfSerachTextIsEmpty = removeCatIfSerachTextIsEmpty;


        var routePageNumber = $stateParams.pageNumber;
        vm.products = products.data;
        vm.selectedProducts = [];
        vm.tableProgressPromise;
        vm.confirmDeleteList = confirmDeleteList;
        vm.categories = categories.data;
        vm.filter = filter;
        vm.query = $location.search();
        angular.extend(vm.query, {
            pageNumber: $stateParams.pageNumber,
            pageSize: ($location.search().pageSize || 15)
        });
        activate();
        vm.getProducts = getProducts;
        vm.showBottomSheetOptions = showBottomSheetOptions;
        ////////////////

        function activate() {
            $rootScope.title = 'لیست محصولات';
            $rootScope.icon="box";
        }

        function getProducts(pageNumber, pageSize) {
            if (pageNumber == routePageNumber &&
                pageSize == $location.search().pageSize) return;
            routePageNumber = pageNumber;

            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            productSrv.get(vm.query).then(function (res) {
                defered.resolve(res.data);
                vm.products = res.data;
            });
            updateRouteParams();
        }

        function updateRouteParams() {
            var query = angular.copy(vm.query)
            query.languageName=undefined;
            query.pageNumber=undefined;
            $location.search(query)
            $state.go($state.current, {
                pageNumber: vm.query.pageNumber,
            }, {
                reload: false
            });
        }

        function filter() {

            vm.query.tagIds = vm.selectedTags.map(function (t) {
                return t.id
            });
            if (vm.selectedCat) {
                vm.query.primaryCategoryId = vm.selectedCat.id;
            } else {
                vm.query.primaryCategoryId = undefined;
            }

            vm.query.pageNumber = 1;
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            productSrv.get(vm.query).then(function (res) {
                defered.resolve(res.data);
                vm.products = res.data;
            });

            updateRouteParams();
        }

        function removeCatIfSerachTextIsEmpty(text) {
            if (text) return;
            vm.selectedCat = undefined;
        }

        function showBottomSheetOptions(product) {
            angular.extend(bottomSheetOptions, {
                locals: {
                    showUrl: product.toFullUrl
                }
            })
            $mdBottomSheet.show(bottomSheetOptions).then(function (res) {
                if (res === 'delete') confirmDelete(product);
                if (res === 'edit') $state.go('productEdit', {
                    id: product.id,
                    lang: $stateParams.lang
                });
            });
        }

        function confirmDelete(product) {
            var confirm = $mdDialog.confirm()
                .title('از انجام عملیات حذف اطمینان دارید؟')
                .textContent(product.title)
                .ok('بلی')
                .cancel('خیر');

            $mdDialog.show(confirm).then(function () {
                productSrv.delete(product.id).then(function (res) {
                    $mdToast.showSimple('انجام شد!');
                    $state.reload();
                });
            });
        }

        function confirmDeleteList() {
            var confirm = $mdDialog.confirm()
                .title('از انجام عملیات حذف اطمینان دارید؟')
                .textContent()
                .ok('بلی')
                .cancel('خیر');

            $mdDialog.show(confirm).then(function () {
                angular.forEach(vm.selectedProducts, function (product) {
                    productSrv.delete(product.id).then(function (res) {
                        vm.selectedProducts.splice(vm.selectedProducts.indexOf(product), 1);
                        if (!vm.selectedProducts.length) {
                            $mdToast.showSimple('انجام شد!');
                            $state.reload();
                        }
                    });
                })
            })
        }
    }
})();