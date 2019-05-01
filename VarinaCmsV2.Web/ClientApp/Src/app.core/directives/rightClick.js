(function () {
    'use strict';

    angular
        .module('app.core')
        .directive('rightClick', rightClick);
        
    rightClick.$inject = ['$parse'];

    function rightClick($parse) {
        return function (scope, element, attrs) {
            var fn = $parse(attrs.rightClick);
            element.bind('contextmenu', function (event) {
                scope.$apply(function () {
                    event.preventDefault();
                    fn(scope, { $event: event });
                });
            });
        };
    }
})();