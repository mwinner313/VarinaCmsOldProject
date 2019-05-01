(function () {
    'use strict';

    angular
        .module('app.core')
        .directive('ngEnter', ngEnter);

    ngEnter.$inject = [];
    function ngEnter() {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            bindToController: true,
            link: link,
            restrict: 'A',
        };
        return directive;

        function link(scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.ngEnter);
                    });

                    event.preventDefault();
                }
            });
        }
    }
})();