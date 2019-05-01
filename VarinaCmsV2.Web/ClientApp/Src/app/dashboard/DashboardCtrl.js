(function () {
    'use strict';

    angular
        .module('app')
        .controller('DashboardCtrl', DashboardCtrl);

    DashboardCtrl.$inject = ["$rootScope", 'widgetsData', '$mdExpansionPanel'];

    function DashboardCtrl($rootScope, widgetsData, $mdExpansionPanel) {
        var vm = this;
        $rootScope.icon = "dashboard";
        $rootScope.title = "داشبورد";
        vm.widgetsData = widgetsData.data;
        activate();

        ////////////////

        function activate() {
            $mdExpansionPanel().waitFor('statistics').then(open);
            $mdExpansionPanel().waitFor('chart').then(open);
            $mdExpansionPanel().waitFor('usersChart').then(open);
            $mdExpansionPanel().waitFor('orderTotalTable').then(open);
            $mdExpansionPanel().waitFor('bestSlling').then(open);
            function open (instance) {
                instance.expand();
            }
        }


    }
})();