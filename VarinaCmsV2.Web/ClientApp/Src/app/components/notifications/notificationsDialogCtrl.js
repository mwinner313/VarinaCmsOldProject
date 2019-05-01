(function() {
    'use strict';

    angular
        .module('app')
        .controller('cotificationDilogCtrl', cotificationDilogCtrl);

    cotificationDilogCtrl.$inject = ['$mdDialog','$sessionStorage'];
    function cotificationDilogCtrl($mdDialog,$sessionStorage) {
        var vm = this;

        activate();
        
        ////////////////
        function activate() { }
    }
})();