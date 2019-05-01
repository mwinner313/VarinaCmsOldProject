(function() {
    'use strict';

    angular
        .module('app')
        .factory('tagDataService', tagDataService);

    tagDataService.$inject = ['$http','api'];
    function tagDataService($http,api) {
       var service = {
            get: get,
        }

        return service;
        ////////////////
        
        function get(query) {
            return $http.get(api.tag, { params: query });
        }
    }
})();