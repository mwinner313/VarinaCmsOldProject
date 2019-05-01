(function () {
    'use strict';
    angular
        .module('app')
        .component('dropDownField', {
            templateUrl: '/Src/app/directives/fields/dropDownField-tpl.html',
            controller: dropDownFieldController,
            controllerAs: 'vm',
            require: '^fieldFactory',
            bindings: {
                model: '=',
                fd: '='
            },
        });

    dropDownFieldController.$inject = ['$timeout', '$element', 'lodash'];
    function dropDownFieldController($timeout, $element, lodash) {
        var vm = this;

        vm.$onInit = function () {
            vm.model = vm.model || {};
            vm.form = $element.parent().controller("form") || $element.parent().parent().controller('form');
            if (vm.model.rawValue) {
                vm.model.rawValue = lodash.find(vm.fd.metaData.avalibleOptions, function (o) { return o.value == vm.model.rawValue.value })
            } else {
                vm.model.rawValue = lodash.find(vm.fd.metaData.avalibleOptions, function (o) { return o.value == vm.fd.defaultValue.value });
            }
        };

        vm.$onChanges = function (changesObj) { };
        vm.$onDestroy = function () { };
    }
})();