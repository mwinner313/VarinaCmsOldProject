(function () {
    'use strict';

    angular
        .module('app')
        .factory('settingSrv', settingSrv);

    settingSrv.$inject = ['$http', 'api'];

    function settingSrv($http, api) {
        var service = {
            getByName : getByName ,
            get: get,
            put: put,
            post: post,
        };

        return service;

        ////////////////
        function get(reqParams) {
            return $http.get(api.setting, {
                params: reqParams
            });
        }

        function post(setting) {
            return $http.post(api.setting, setting);
        }

        function put(setting) {
            return $http.put(api.setting + '/' + setting.id, setting);
        }

        function getByName (name) {
            return $http.get(api.setting + '/' + name);
        }

    }
})();