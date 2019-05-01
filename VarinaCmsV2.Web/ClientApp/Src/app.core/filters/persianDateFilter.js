(function () {
    'use strict';

    angular
        .module('app.core')
        .filter('persianDate', persianDateF);

    function persianDateF() {
        return persianDateFilter;

        ////////////////

        function persianDateFilter(date, format) {
            if (!format) return persianDate(date).format("dddd, YYYY/M/D, HH:mm ");
             return persianDate(date).format(format)
        }
    }
})();