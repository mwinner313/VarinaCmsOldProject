(function() {
    'use strict';

    angular
        .module('app')
        .factory('roleSrv', roleSrv);

    roleSrv.$inject = ['$http','api'];
    function roleSrv($http,api) {
        var service = {
            get:get
        };
        
        return service;

        ////////////////
        function get(params) {
            return $http.get(api.role,{params:params});
         }
    }
})();