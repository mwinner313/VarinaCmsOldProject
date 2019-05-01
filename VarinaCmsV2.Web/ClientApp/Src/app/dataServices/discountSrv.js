(function() {
    'use strict';

    angular
        .module('app')
        .factory('discountSrv', discountSrv);

    discountSrv.$inject = ['$http','api'];
    function discountSrv($http,api) {
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
            return $http.get(api.discount, {
                params: reqParams
            })
        }

        function getById(id) {
            return $http.get(api.discount + '/' + id);
        }

        function post(discount) {
            return $http.post(api.discount, discount);
        }

        function put(discount) {
            return $http.put(api.discount + '/' + discount.id, discount);
        }

        function remove(id) {
            return $http.delete(api.discount + '/' + id);
        }
    }
})();