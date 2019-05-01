(function() {
    'use strict';

    angular
        .module('app')
        .factory('dashboardSrv', dashBoardSrv);

    dashBoardSrv.$inject = ['$http','api'];
    function dashBoardSrv($http,api) {
        var service = {
            get:get
        };
        
        return service;

        ////////////////
        function get() { 
            return $http.get(api.dashboard)
        }
    }
})();