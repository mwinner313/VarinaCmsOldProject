(function () {
    'use strict';

    angular
        .module('app')
        .service('currentLang', currentLang);

    currentLang.$inject = [];
    function currentLang() {
        this.val = 'fa-IR';
        this.set = set;
        this.get = get;
        function set(val) {
            this.val = val;
        }

        function get() {
            return this.val;
        }
    }
})();