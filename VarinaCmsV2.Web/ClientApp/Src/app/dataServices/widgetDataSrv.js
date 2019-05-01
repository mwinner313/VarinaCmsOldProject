(function () {
    'use strict';

    angular
        .module('app')
        .factory('widgetDataSrv', widgetDataSrv);

    widgetDataSrv.$inject = ['$http', 'api'];
    function widgetDataSrv($http, api) {
        var service = {
            post: post,
            delete:remove,
            put:put
        };

        return service;

        ////////////////
        function post(data) {
            return $http.post(api.widget, data);
        }
        function remove(id) {
            return $http.delete(api.widget+'/'+id);
        }
        function put(data) {
            return $http.put(api.widget+'/'+data.id, data);
        }
    }
})();