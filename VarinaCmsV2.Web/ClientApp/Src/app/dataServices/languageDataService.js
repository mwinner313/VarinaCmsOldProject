(function() {
    'use strict';

    angular
        .module('app')
        .factory('langDataSrv', langDataSrv);

    langDataSrv.$inject = ['$http','api'];
    function langDataSrv($http,api) {
        var service = {
            get:get
        };
        
        return service;

        ////////////////
        function get() { 
            return $http.get(api.language);
        }
    }
})();