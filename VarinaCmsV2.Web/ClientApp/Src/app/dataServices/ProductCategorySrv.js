(function () {
    'use strict';

    angular
        .module('app')
        .factory('productCategorySrv', productCategoryDataService);

    productCategoryDataService.$inject = ['$http', 'api'];
    function productCategoryDataService($http, api) {
        var service = {
            get: get,
            put: put,
            putUpdateChildParent: putUpdateChildParent,
            post: post,
            delete: remove,
        };

        return service;

        ////////////////
        function get(params) {
            return $http.get(api.productCategory, { params: params });
        }
        function post(productCategory) {
            return $http.post(api.productCategory, productCategory);
        }

        function remove(productCategory) {
            return $http.delete(api.productCategory + '/' + productCategory.id);
        }
        function put(productCategory) {
            return $http.put(api.productCategory + '/' + productCategory.id, productCategory);
        }
        function putUpdateChildParent(categories) {
            return $http.put(api.productCategory + '/UpdateChildParent', categories);
        }
    }
})();