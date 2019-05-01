(function() {
    'use strict';

    angular
        .module('app.core')
        .filter('dashremove', dashremove);

    function dashremove() {
        return dashremoveFilter;

        ////////////////

        function dashremoveFilter(str) {
            return str.split('-').join('');
        }
    }
})();