(function () {
    'use strict';

    angular
        .module('app.core')
        .directive('focusOnClick', focusOnClick);

    focusOnClick.$inject = ["$timeout"];
    function focusOnClick($timeout) {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            $(element).click(function () {
                $timeout(function () {
                    var $elToFocus = $("[name='" + attrs.focusOnClick + "']");
                    $elToFocus.focus();

                }, 200)
            })

        }
    }
})();