(function () {
    'use strict';

    angular
        .module('app')
        .factory('entityDataService', entityDataService);

    entityDataService.$inject = ['$http', 'api'];
    function entityDataService($http, api) {
        var service = {
            getById: getById,
            get: get,
            put: put,
            post: post,
            delete: remove
        };

        return service;

        ////////////////
        function get(reqParams) {
            return $http.get(api.entity, { params: reqParams })
        }
        function getById(scheme,id) {
            return $http.get(api.entity + '/' + scheme + '/' + id);
        }
        function post(entity) {
            return $http.post(api.entity, entity);
        }

        function put(entity,scheme) {
            return $http.put(api.entity+ '/' +scheme.handle + '/' + entity.id, entity);
        }

        function remove(scheme, id) {
            return $http.delete(api.entity + '/' + scheme + '/' + id);
        }
    }
})();