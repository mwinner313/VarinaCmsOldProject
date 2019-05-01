(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('notSeenForms', {
            templateUrl: '/Src/app/components/notifications/components/notSeenForm.tpl.html',
            controller: ControllerController,
            controllerAs: 'vm',
            bindings: {
            },
        });

    ControllerController.$inject = ["$sessionStorage", 'lodash','formDataSrv'];
    function ControllerController($sessionStorage, lodash,formDataSrv) {
        var vm = this;
        vm.changeIsSeenState = changeIsSeenState;
        vm.notSeenForms = [];
        var formGroups = lodash.groupBy($sessionStorage.notSeenForms.items, 'formSchemeTitle');
        Object.keys(formGroups)
            .map(function (key) {
                vm.notSeenForms.push({ scheme: key, items: formGroups[key] })
            });

        ////////////////

        vm.$onInit = function () {
        };
        vm.$onChanges = function (changesObj) { };
        vm.$onDestroy = function () { };

        function changeIsSeenState(form) {
            formDataSrv.changeIsSeenState(form.id).then(function (res) {
                form.isSeen = !form.isSeen;
            });
        }
    }
})();