(function () {
    'use strict';

    // Usage:   
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('dropDownFieldConfiguration', {
            templateUrl: '/Src/app/directives/fieldConfiguration/dropDownFieldConfiguration.tpl.html',
            controller: dropDownFieldConfigurationCtrl,
            controllerAs: 'vm',
            bindings: {
                fd: '=',
            },
        });

    dropDownFieldConfigurationCtrl.$inject = [];
    function dropDownFieldConfigurationCtrl() {
        var vm = this;


        ////////////////

        vm.$onInit = function () {
            vm.fd.metaData=vm.fd.metaData||{};
            vm.fd.metaData.avalibleOptions = vm.fd.metaData.avalibleOptions || [{}];
        };
        vm.$onChanges = function (changesObj) { };
        vm.$onDestroy = function () { };
    }
})();