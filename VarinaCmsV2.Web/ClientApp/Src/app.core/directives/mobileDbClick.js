(function () {
    'use strict';

    angular
        .module('app.core')
        .directive('mobileDbClick', mobileDbClick);

    mobileDbClick.$inject = [];
    function mobileDbClick() {
        const DblClickInterval = 300; //milliseconds

        var firstClickTime;
        var waitingSecondClick = false;

        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                element.bind('click', function (e) {

                    if (!waitingSecondClick) {
                        firstClickTime = (new Date()).getTime();
                        waitingSecondClick = true;

                        setTimeout(function () {
                            waitingSecondClick = false;
                        }, DblClickInterval);
                    }
                    else {
                        waitingSecondClick = false;

                        var time = (new Date()).getTime();
                        if (time - firstClickTime < DblClickInterval) {
                            scope.$apply(attrs.mobileDbClick);
                        }
                    }
                });
            }
        };
    }
})();