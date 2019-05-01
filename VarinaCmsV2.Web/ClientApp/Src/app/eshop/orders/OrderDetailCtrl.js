(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderDetailCtrl', OrderDetailCtrl);

    OrderDetailCtrl.$inject = ['productEditorSetting', '$rootScope','order', 'orderSrv', '$mdToast', '$mdDialog', 'datetimeRegex', '$q'];


    function OrderDetailCtrl(productEditorSetting,$rootScope,order, orderSrv, $mdToast, $mdDialog, datetimeRegex, $q) {
        var vm = this;
        var map;
        vm.datetimeRegex = datetimeRegex;
        vm.order = order.data;
        vm.update = update;
        vm.showItemDetail = showItemDetail;
        vm.updateShippingStatus = updateShippingStatus;
        vm.updatePaymentStatus = updatePaymentStatus;
        vm.initShippingAddressGoogleMap = initShippingAddressGoogleMap;
        vm.productEditorSetting=productEditorSetting.data;
        function activate() {
            if (!vm.order.shipmentId && vm.order.shippingStatus !== 10) {
                vm.order.shipment = {};
            }
            $rootScope.title = "جزییات سفارش";
            $rootScope.icon = "checklist";
        }

        activate();

        function initShippingAddressGoogleMap() {
            if (!vm.order.shippingAddress) return;
            if (!vm.order.shippingAddress.mapLatLang) return;

            var stat = vm.order.shippingAddress.mapLatLang.split(',');
            map = new google.maps.Map(document.getElementById('map'), {
                center: {
                    lat: stat[0] * 1,
                    lng: stat[1] * 1
                },
                zoom: 15,
                scrollwheel: false
            });

            var marker = new google.maps.Marker({
                position: {
                    lat: stat[0] * 1,
                    lng: stat[1] * 1
                },
                map: map
            });
        }

        function update() {
            if (vm.orderForm.$invalid) {
                $mdToast.showSimple('مقادیر را بررسی کنید!');
                return;
            }
            var defered = $q.defer();
            orderSrv.put(vm.order).then(function (res) {
                if (res.status == 200) {
                    vm.order.orderLogs = res.data.logs;
                    $mdToast.showSimple('انجام شد!')
                    defered.resolve(res);
                } else {
                    defered.reject(res);
                }
            });
            return defered.promise;
        }

        function updateShippingStatus(status) {
            vm.order.shippingStatus = status;
            vm.shippingStatusEditMode = false
            update().then(function (res) {
                if (res.status == 200 &&status==30) {
                    $mdDialog.show($mdDialog.confirm()
                        .title('اعلام تغییر وضعیت ارسال به سفارش دهنده')
                        .content('مایلید این تغییر وضعیت به سفارش دهنده اطلاع رسانی شود')
                        .ok('بلی')
                        .cancel('خیر')).then(function (res) {
                      orderSrv.sendOrderShippmentStatusChangedNotification(vm.order.id)
                    });
                }
            });
        }

        function updatePaymentStatus(status) {
            vm.order.paymentStatus = status;
            vm.paymentStatusEditMode = false
            update();
        }

        function showItemDetail(item, $event) {
            $mdDialog.show({
                controller: 'OrderItemDetailCtrl',
                controllerAs: 'vm',
                targetEvent: $event,
                templateUrl: '/Src/app/eshop/orders/orderItemDetail.tpl.html',
                bindToController: true,
                fullscreen: true,
                clickOutsideToClose: true,
                locals: {
                    item: item
                }
            }).then(function () {
                update();
            });
        };
    }
})();