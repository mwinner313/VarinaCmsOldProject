(function () {
    'use strict';
    angular
        .module('app')
        .component('commonStatistics', {
            templateUrl: '/Src/app/dashboard/components/CommonStatisticsComponent.tpl.html',
            controller: CommonStatisticCtrl,
            controllerAs: 'vm',
            bindings: {
                widgetDatas: "<"
            },
        });

    CommonStatisticCtrl.$inject = ['lodash', '$state','$rootScope'];

    function CommonStatisticCtrl(lodash, $state,$rootScope) {
        var vm = this;

        ////////////////

        vm.$onInit = function () {
            vm.data = lodash.find(vm.widgetDatas, function (widgetData) {
                return widgetData.name === "CommonStatistics";
            }).data;
        };


    }
})();