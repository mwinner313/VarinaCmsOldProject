(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('numberField', {
            templateUrl: '/Src/app/directives/fields/numberField-tpl.html',
            controller: Controller,
            require: '^fieldFactory',
            controllerAs: 'vm',
            bindings: {
                model: '=',
                fd: '=',
                disableRequiredValidation: '<'

            },
        });

    Controller.$inject = ['$timeout', '$element'];
    function Controller($timeout, $element) {
        var vm = this;
        vm.isFieldRequired = isFieldRequired;
        vm.form = $element.parent().controller("form") || $element.parent().parent().controller('form');
        $timeout(function () {
            vm.field = vm.form[vm.fd.handle];
        })

        ////////////////
        function isFieldRequired() {
            if (vm.disableRequiredValidation) return false;
            return vm.fd.isRequired;
        }
    }
})();