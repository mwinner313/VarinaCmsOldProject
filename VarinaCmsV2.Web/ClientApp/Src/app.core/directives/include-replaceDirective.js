(function () {
    'use strict'
    angular
        .module('app.core')
        .directive('replace', function () {
            return {
                restrict: 'A', /* optional */
                link: function (scope, el, attrs) {
                   $(el) .replaceWith(el.children());
                }
            };
        });
}())

