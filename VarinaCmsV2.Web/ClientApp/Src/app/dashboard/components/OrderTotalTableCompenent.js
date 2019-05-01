(function () {
    'use strict';
    angular
        .module('app')
        .component('orderTotalTable', {
            templateUrl: '/Src/app/dashboard/components/OrderTotalTableCompenent.tpl.html',
            controller: ControllerController,
            controllerAs: 'vm',
            bindings: {
                widgetDatas: "<"
            },
        });

    ControllerController.$inject = ['lodash'];

    function ControllerController(lodash ) {
        var vm = this;

        vm.$onInit = function () {
            vm.data = lodash.find(vm.widgetDatas, function (widgetData) {
                return widgetData.name === "OrderTotals";
            }).data;
        };

    }
})();