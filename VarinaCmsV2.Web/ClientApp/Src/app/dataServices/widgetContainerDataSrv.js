(function () {
    'use strict';

    angular
        .module('app')
        .factory('widgetContainerDataSrv', widgetContainerDataSrv);

    widgetContainerDataSrv.$inject = ['$http', 'api'];
    function widgetContainerDataSrv($http, api) {
        var service = {
            get: get,
            put: put,
            post: post,
            delete:remove
        };

        return service;

        ////////////////
        function get() {
            return $http.get(api.widgetContainer);
        }
        
        function put(data) {
            return $http.put(api.widgetContainer + '/' + data.id, data);
        }
        
        function post(data) {
            return $http.post(api.widgetContainer, data);
        }
        
        function remove(id) {
            return $http.delete(api.widgetContainer + '/' + id);
        }
    }
})();