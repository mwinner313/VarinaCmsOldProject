(function () {
    'use strict';

    angular
        .module('app')
        .factory('userDataSrv', userDataSrv);

    userDataSrv.$inject = ['$http', 'api'];
    function userDataSrv($http, api) {
        var service = {
            get: get,
            getById: getById,
            add: add,
            update: update,
            delete: remove
        };

        return service;

        ////////////////
        function get(params) {
            return $http.get(api.user, { params: params });
        }
        function getById(id) {
            return $http.get(api.user + '/' + id);
        }
        function add(user) {
            return $http.post(api.user, user);
        }
        function update(user) {
            return $http.put(api.user+"/"+user.id, user);
        }
        function remove(id) {
            return $http.delete(api.user + '/' + id);
        }
    }
})();