(function () {
    'use strict';

    angular
        .module('angular-nicescroll', [])
        .directive('ngNicescroll', ngNicescroll);

    ngNicescroll.$inject = ['$rootScope','$parse'];

    /* @ngInject */
    function ngNicescroll($rootScope,$parse) {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            link: link
        };
        return directive;

        function link(scope, element, attrs, controller) {

            function init(){
                var niceOption = scope.$eval(attrs.niceOption)
                
                            var niceScroll = $(element).niceScroll(angular.extend(niceOption,{emulatetouch: true,mousescrollstep:10,scrollspeed:30,cursordragspeed: 2}));
                            var nice = $(element).getNiceScroll();
                          
                            if (attrs.niceScrollObject)  $parse(attrs.niceScrollObject).assign(scope, nice);
                       
                            // on scroll end
                            niceScroll.onscrollend = function (data) {
                                if (this.newscrolly >= this.page.maxh) {
                                    if (attrs.niceScrollEnd) scope.$evalAsync(attrs.niceScrollEnd);
                
                                }
                                if (data.end.y <= 0) {
                                    // at top
                                    if (attrs.niceScrollTopEnd) scope.$evalAsync(attrs.niceScrollTopEnd);
                                }
                            };
                
                
                            scope.$on('$destroy', function () {
                                if (angular.isDefined(niceScroll.version)) {
                                    niceScroll.remove();
                                }
                            })
            }
            init();
           
        }
    }


})();
