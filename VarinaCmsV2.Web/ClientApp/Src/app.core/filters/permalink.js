(function () {
    'use strict';

    angular
        .module('app.core')
        .filter('permalink', permalink);

    function permalink() {
        return permalinkFilter;

        ////////////////

        function permalinkFilter(str) {
            if(['',null, undefined].indexOf(str)!==-1)
            return "";
            var re = /[^a-z0-9ا-ی]+/gi; // global and case insensitive matching of non-char/non-numeric
            var re2 = /^-*|-*$/g;     // get rid of any leading/trailing dashes
            str = str.replace(re, '-');  // perform the 1st regexp
            return str.replace(re2, '').toLowerCase(); // ..aaand the second + return lowercased result
        }
    }
})();   