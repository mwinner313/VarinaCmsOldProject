(function () {
    'use strict';

    angular
        .module('app')
        .directive('fieldConfigurationFactory', fieldFactory);

    fieldFactory.$inject = [];
    function fieldFactory() {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            bindToController: true,
            controller: fieldConfigurationFactory,
            templateUrl: '/Src/app/directives/fieldConfigurationFactory-tpl.html',
            controllerAs: 'vm',
            link: link,
            restrict: 'E',
            require: '^form',
            scope: {
                fd: '=',
            }
        };

        return directive;

        function link(scope, element, attrs) {

        }
    }
    /* @ngInject */
    fieldConfigurationFactory.$inject = [];
    function fieldConfigurationFactory() {
        var vm = this;
    }

})();