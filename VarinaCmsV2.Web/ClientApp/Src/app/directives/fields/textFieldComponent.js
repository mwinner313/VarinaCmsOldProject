(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('textField', {
            templateUrl: '/Src/app/directives/fields/textField-tpl.html',

            controller: Controller,
            controllerAs: 'vm',
            require: '^fieldFactory',
            bindings: {
                model: '=',
                fd:'=',
                disableRequiredValidation:'<'
            },
        });

    Controller.$inject = ['$element', '$timeout'];
    function Controller($element, $timeout) {
        var vm = this;
        var formCtrl = $element.parent().controller('form') || $element.parent().parent().controller('form');
        vm.form = formCtrl;
        vm.isFieldRequired=isFieldRequired;
        $timeout(function () {
            vm.field = formCtrl[vm.fd.handle];
        });

        ////////////////
         function isFieldRequired() {
            if (vm.disableRequiredValidation) return false;
            return vm.fd.isRequired;
        }
    }
})();