(function() {
    'use strict';

    angular
        .module('app')
        .factory('schemeDataService', schemeDataService);

    schemeDataService.$inject = ['$http','api'];
    function schemeDataService($http,api) {
        var service = {
            get: get,
            getBySchemeHandle: getBySchemeHandle,
            put: put,
            post: post,
            delete: remove
        }

        return service;
        ////////////////

        function getBySchemeHandle(handle) {
            return $http.get(api.scheme + '/' + handle);
        }
        
        function get(query) {
            return $http.get(api.scheme, { params: query });
        }

        function post(scheme) {
            return $http.post(api.scheme, scheme);
        }

        function put(scheme) {
            return $http.put(api.scheme + '/' + scheme.id, scheme);
        }
        
        function remove(id) {
            return $http.delete(api.scheme + '/' + id);
        }
    }
})();