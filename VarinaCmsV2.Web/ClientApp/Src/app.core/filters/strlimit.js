(function () {
    'use strict';

    angular
        .module('app.core')
        .filter('strlimit', function () {
            return function (str, count) {
                if(!str)return;
                return (count ? str.slice(0, count) : str)+((count<str.length)?"...":"");
            }
        });
})();