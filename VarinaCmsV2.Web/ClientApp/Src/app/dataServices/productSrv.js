(function () {
    'use strict';

    angular
        .module('app')
        .factory('productSrv', productSrv);

    productSrv.$inject = ['$http', 'api'];

    function productSrv($http, api) {
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
            return $http.get(api.product, {
                params: reqParams
            })
        }

        function getById(id) {
            return $http.get(api.product + '/' + id);
        }

        function post(product) {
            return $http.post(api.product, product);
        }

        function put(product) {
            return $http.put(api.product + '/' + product.id, product);
        }

        function remove(id) {
            return $http.delete(api.product + '/' + id);
        }
    }
})();