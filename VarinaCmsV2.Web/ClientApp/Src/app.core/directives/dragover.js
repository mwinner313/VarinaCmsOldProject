(function() {
    'use strict';

    angular
        .module('app.core')
        .directive('dragover', dragover);

    dragover.$inject = [];
    function dragover() {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            link: link,
            restrict: 'A',
        };
        return directive;
        
        function link(scope, element, attrs) {
            $(element).on('dragenter', function (event) {
                event.preventDefault();
                scope.$apply(function () {
                    scope.$eval(attrs.dragover);
                });
            });
        }
    }
})();