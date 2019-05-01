(function () {
    'use strict';

    angular
        .module('app.core')
        .directive('loadingBar', loadingBar);

    loadingBar.$inject = ['$transitions', '$compile'];
    function loadingBar($transitions, $compile) {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            link: link,
            restrict: 'A',
            scope: {}

        };
        return directive;

        function link(scope, el, attrs) {
            $transitions.onStart({}, function ($transition$) {

                var loader = $compile(
                    '<md-progress-circular id="loading-el" md-mode="indeterminate" class="loading md-warn md-accent"></md-progress-circular>')(scope);

                $(el).children().each(function () {
                    $(this).css("display", "none");
                });

                $(el).append(loader);
            });

            $transitions.onSuccess({}, function ($transition$) {
                $(el).children().each(function () {
                    $(this).css("display", "block");
                })
                $('#loading-el').remove();
            });
        }
    }
    /* @ngInject */
    function ControllerController() {

    }
})();