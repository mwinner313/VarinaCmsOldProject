(function() {
    'use strict';

    angular
        .module('app.core')
        .directive('niceScroll', niceScroll);

    niceScroll.$inject = ['$timeout'];
    function niceScroll($timeout) {
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
            $timeout(function(){
                $(element).niceScroll()
            },0);
        }
    }
  

})();