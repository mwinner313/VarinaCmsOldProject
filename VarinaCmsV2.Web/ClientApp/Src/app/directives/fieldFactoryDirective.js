(function () {
    'use strict';

    angular
        .module('app')
        .directive('fieldFactory', fieldFactory);

    fieldFactory.$inject = [];
    function fieldFactory() {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            bindToController: true,
            controller: Controller,
            templateUrl: '/Src/app/directives/fieldFactory-tpl.html',
            controllerAs: 'vm',
            link: link,
            restrict: 'E',
            require: '^form',
            scope: {
                model: '=',
                fd: '=',
            }
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }
    /* @ngInject */
    Controller.$inject = ['$timeout','$attrs'];
    function Controller($timeout, $attrs) {
        var vm = this;
        vm.disableRequiredValidation = $attrs.disableRequiredValidation === "true";
        $timeout(function () {
            vm.model = vm.model || {};
            if (vm.fd) {
                angular.extend(vm.model, {
                    fieldDefenitionId: vm.fd.id,
                    fieldDefenitionTitle: vm.fd.title,
                    fieldDefenitionType: vm.fd.type,
                });
            }
        })
    }

})();