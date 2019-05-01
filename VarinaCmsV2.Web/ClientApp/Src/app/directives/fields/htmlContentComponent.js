(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('htmlContentField', {
            templateUrl: '/Src/app/directives/fields/htmlContentField-tpl.html',
            controller: Controller,
            controllerAs: 'vm',
            bindings: {
                model: '=',
                fd: '=',
                disableRequiredValidation: "<"

            },
        });

    Controller.$inject = ['$element', '$timeout', 'tinyMceOptions'];

    function Controller($element, $timeout, tinyMceOptions) {
        var vm = this;
        vm.isFieldRequired = isFieldRequired;
        vm.tinyMceOptions = tinyMceOptions;
        vm.form = $element.parent().controller('form') || $element.parent().parent().controller('form');
        $timeout(function () {
            vm.field = vm.form[vm.fd.handle];
        });

        ////////////////
        function isFieldRequired() {
            if (vm.disableRequiredValidation) return false;
            return vm.fd.isRequired;
        }
        vm.$onInit = function () {
            vm.model=vm.model||{};
            vm.model.rawValue=vm.model.rawValue||{};
            vm.model.rawValue.content = vm.model.rawValue.content || ' ';
        };
        vm.$onChanges = function (changesObj) {};
        vm.$onDestroy = function () {};
    }
})();