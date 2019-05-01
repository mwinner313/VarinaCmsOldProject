(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('menuItemPlugins', {
            templateUrl: '/Src/app/menu/menuItemPluginComponents.tpl.html',
            controller: ControllerController,
            controllerAs: 'vm',
            bindings: {
                item: '=',
            },
        });

    ControllerController.$inject = [];
    function ControllerController() {
        var vm = this;


        ////////////////

        vm.$onInit = function () {
            if (!vm.item.metaData)
                vm.item.metaData = [];
        };
        
        vm.$onChanges = function (changesObj) { };
        vm.$onDestroy = function () { };
    }
})();