(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('bestSellings', {
            templateUrl: '/Src/app/dashboard/components/BestSellingsComponent.tpl.html',
            controller: ControllerController,
            controllerAs: 'vm',
            bindings: {
                widgetDatas: "<"
            },
        });

    ControllerController.$inject = ['lodash'];

    function ControllerController(lodash) {
        var vm = this;

        vm.$onInit = function () {
            vm.data = lodash.find(vm.widgetDatas, function (widgetData) {
                return widgetData.name === "BestSellings";
            }).data;
         vm.mode = Object.keys(vm.data)[0];
         vm.modes = Object.keys(vm.data);
        };

    }
})();