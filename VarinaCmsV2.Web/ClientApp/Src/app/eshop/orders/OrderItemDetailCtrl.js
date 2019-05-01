(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderItemDetailCtrl', OrderItemDetailCtrl);

    OrderItemDetailCtrl.$inject = ['$mdDialog', 'orderSrv'];

    function OrderItemDetailCtrl($mdDialog, orderSrv) {
        var vm = this;
        vm.changeDownloadActivationState = changeDownloadActivationState;
        activate();

        ////////////////

        function activate() {}
        function changeDownloadActivationState(){
            orderSrv.changeOrderItemDownLoadActivationState(vm.item).then(function(res){
                vm.item.isDownloadActivated=!vm.item.isDownloadActivated;
                $mdDialog.hide();
            });
        }        
    }
})();