(function () {
    'use strict';


    angular
        .module('app')
        .component('datetimeField', {
            templateUrl: '/Src/app/directives/fields/datetimeField-tpl.html',
            controller: Controller,
            require: '^fieldFactory',
            controllerAs: 'vm',
            bindings: {
                model: '=',
                justDate: '<',
                disableRequiredValidation: '<',
                fd: '='
            },
        });

    Controller.$inject = ['$timeout', '$element', 'datetimeRegex', 'dateRegex'];

    function Controller($timeout, $element, datetimeRegex, dateRegex) {
        var vm = this;
        vm.datetimeRegex = datetimeRegex;
        vm.dateRegex = dateRegex;
        vm.form = $element.parent().controller("form") || $element.parent().parent().controller('form');
        vm.isFieldRequired = isFieldRequired
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