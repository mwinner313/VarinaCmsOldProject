(function () {
    'use strict';

    angular
        .module('app.core')
        .directive('persianDate', persianDateDirective);

    persianDateDirective.$inject = ['$parse', '$timeout'];

    function persianDateDirective($parse, $timeout) {
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
            var parsed = $parse(attrs.ngModel);
            var modelValue = parsed(scope);
            var format = attrs.justDate == 'true' ? "YYYY/MM/DD" : "YYYY/MM/DD - HH:mm";
            $(element).persianDatepicker({
                observer: true,
                format: format,
                timePicker: {
                    enabled: !attrs.justDate
                },
                onSelect: function (unix) {
                    scope.$apply(function () {

                    });
                }

            });

            // function toEnglishDigits(str) {
            //     var id = {
            //         '۰': '0',
            //         '۱': '1',
            //         '۲': '2',
            //         '۳': '3',
            //         '۴': '4',
            //         '۵': '5',
            //         '۶': '6',
            //         '۷': '7',
            //         '۸': '8',
            //         '۹': '9'
            //     };
            //     return str.replace(/[^0-9.]/g, function (w) {
            //         return id[w] || w;
            //     });
            // };

            // var now = persianDate(new Date()).format(format).toString();

             parsed.assign(scope, (modelValue || undefined));


        }
    }

})();