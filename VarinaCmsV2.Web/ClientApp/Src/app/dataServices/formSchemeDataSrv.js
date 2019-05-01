(function () {
    'use strict';

    angular
        .module('app')
        .factory('formSchemeDataSrv', formSchemeDataSrv);

    formSchemeDataSrv.$inject = ['$http', 'api'];
    function formSchemeDataSrv($http, api) {
        var service = {
            get: get,
            getByHandle: getByHandle,
            post: post,
            put: put,
            delete: remove
        };

        return service;

        ////////////////
        
        function get(params) {
            return $http.get(api.formScheme, { params: params });
        }
        function getByHandle(handle) {
            return $http.get(api.formScheme + '/' + handle);
        }
        function post(data) {
            return $http.post(api.formScheme, data);
        }
        function put(data) {
            return $http.put(api.formScheme + '/' + data.id, data);
        }
        function remove(id) {
            return $http.delete(api.formScheme + '/' + id);
        }

    }
})();