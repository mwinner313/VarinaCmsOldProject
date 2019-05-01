(function () {
    'use strict';

    angular
        .module('app')
        .controller('DiscountListCtrl', DiscountListCtrl);

    DiscountListCtrl.$inject = ['discountSrv', '$mdBottomSheet', 'dicsounts', '$state', '$rootScope','$location','$q','$stateParams'];

    function DiscountListCtrl(discountSrv, $mdBottomSheet, discounts, $state, $rootScope,$location,$q,$stateParams) {
        var routePageNumber = $stateParams.pageNumber;
        var vm = this;
        vm.query = {
            pageNumber: $stateParams.pageNumber,
            pageSize: ($location.search().pageSize || 15)
        }

        vm.filter = filter
        vm.goToDetails = goToDetails;
        vm.discounts = discounts.data;
        vm.getDiscounts = getDiscounts;

        function filter() {
            vm.query.pageNumber = 1;
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            discountSrv.get(vm.query).then(function (res) {
                defered.resolve(res.data);
                vm.discounts = res.data;
            });
            updateRouteParams();
        }

        function goToDetails(discount) {
            $state.go('discountDetails', {
                lang: $rootScope.currentLang,
                id: discount.id
            });
        }

        function getDiscounts(pageNumber,pageSize) {
            if (pageNumber == routePageNumber &&
                pageSize == $location.search().pageSize) return;
            routePageNumber = pageNumber;
            
            var defered = $q.defer();

            vm.tableProgressPromise = defered.promise;

            discountSrv.get(vm.query).then(function (res) {
                defered.resolve(res.data);
                vm.discounts = res.data;
            });

            updateRouteParams();
        }

        function updateRouteParams() {
            $location.search({
                pageSize: vm.query.pageSize,
            })
            $state.go($state.current, {
                pageNumber: vm.query.pageNumber,
            }, {
                reload: false
            });
        }
    }
})();