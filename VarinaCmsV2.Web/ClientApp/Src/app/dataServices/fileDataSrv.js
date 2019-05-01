(function() {
    'use strict';

    angular
        .module('app')
        .factory('fileDataSrv', fileDataService);

    fileDataService.$inject = ['$http','api'];
    function fileDataService($http,api) {
        var service = {
            get: get,
            post: post,
            put: put,
            delete: remove,
        };
        function get(params) {
            return $http.get(api.file,{params:params});
        }
        function post(file) {
            return $http.post(api.file, file);
        }

        function put(file) {
            return $http.put(api.file + '/' + file.id, file);
        }

        function remove(id) {
            return $http.delete(api.file + '/' + id);
        }

      
        return service;

        ////////////////
    }
})();