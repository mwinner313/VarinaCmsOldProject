(function () {
    'use strict';

    angular
        .module('app')
        .factory('fdDataService', fdDataService);

    fdDataService.$inject = ['$http', 'api'];
    function fdDataService($http, api) {
        var service = {
            post: post,
            put: put,
            delete: remove,
            putBatch:putBatch,
            putFdGroup:putFdGroup,
            deleteFdGroup:deleteFdGroup,
            postFdGroup:postFdGroup,
        };

        return service;

        ////////////////
        function post(fd) {
            return $http.post(api.fieldDefenition, fd);
        }
        function put(fd) {
            return $http.put(api.fieldDefenition+'/'+fd.id, fd);
        }
        function putBatch(fds) {
            return $http.put(api.fieldDefenition+'/putbatch', fds);
        }
        function remove(id) {
            return $http.delete(api.fieldDefenition+'/'+id);
        }
        function putFdGroup(group){
            return $http.put(api.fieldDefenition+'/updateFdGroup/'+group.id, group);
        }
        function deleteFdGroup(group){
            return $http.delete(api.fieldDefenition+'/deleteFdGroup/'+group.id);
        }
        function postFdGroup(group){
            return $http.post(api.fieldDefenition+'/postFdGroup',group);
        }
    }
})();