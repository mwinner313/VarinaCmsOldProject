(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderListCtrl', OrderListCtrl);

    OrderListCtrl.$inject = ['productEditorSetting', '$rootScope', '$q', 'orderSrv', 'datetimeRegex', 'orders', '$stateParams', '$location', '$state'];

    function OrderListCtrl(productEditorSetting, $rootScope, $q, orderSrv, datetimeRegex, orders, $stateParams, $location, $state) {
        var routePageNumber = $stateParams.pageNumber;

        var vm = this;
        vm.productEditorSetting = productEditorSetting.data;
        vm.selectedOrders = [];
        vm.goToDetails = goToDetails;
        vm.datetimeRegex = datetimeRegex;
        vm.orders = orders.data;
        vm.filter = filter;
        vm.query = $location.search()
        angular.extend(vm.query, {
            pageNumber: $stateParams.pageNumber,
            pageSize: ($location.search().pageSize || 15)
        })

        vm.getOrders = getOrders;
        activate();


        function activate() {
            $rootScope.title = "مدیریت سفارشات";
            $rootScope.icon = "notepad";
        }


        function goToDetails(order) {
            orderSrv.changeSeenStateByAdmin(order);
            $state.go('orderDetails', {
                id: order.id,
                lang: $stateParams.lang
            })
        }

        function filter() {
            vm.query.pageNumber = 1;
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            orderSrv.get(vm.query).then(function (res) {
                defered.resolve(res.data);
                vm.orders = res.data;
            });
            updateRouteParams();
        }

        function getOrders(pageNumber, pageSize) {
            if (pageNumber == routePageNumber &&
                pageSize == $location.search().pageSize) return;
            routePageNumber = pageNumber;

            var defered = $q.defer();

            vm.tableProgressPromise = defered.promise;

            orderSrv.get(vm.query).then(function (res) {
                defered.resolve(res.data);
                vm.orders = res.data;
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
    }
})();