(function () {
    'use strict';

    angular
        .module('app')
        .factory('pageDataService', pageDataService);

    pageDataService.$inject = ['$http', 'api'];
    function pageDataService($http, api) {
        var service = {
            get: get,
            getById: getById,
            put: put,
            post: post,
            delete: remove
        }

        return service;
        ////////////////

        function getById(id) {
            return $http.get(api.page + '/' + id);
        }
        
        function get(query) {
            return $http.get(api.page, { params: query });
        }

        function post(page) {
            return $http.post(api.page, page);
        }

        function put(page, id) {
            return $http.put(api.page + '/' + id, page);
        }
        
        function remove(id) {
            return $http.delete(api.page + '/' + id);
        }
    }
})();