(function () {
    'use strict';

    angular
        .module('app.core')
        .filter('highlight', highlight);

    function highlight($sce) {
        return highlightFilter;

        ////////////////

        function highlightFilter(text, phrase) {
            if (phrase) text = text.replace(new RegExp('(' + phrase + ')', 'gi'),
                '<span class="highlighted">$1</span>')
            return text;
        }
    }
})();