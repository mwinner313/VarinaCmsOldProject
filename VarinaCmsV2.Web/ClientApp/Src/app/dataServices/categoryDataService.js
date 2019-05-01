(function () {
    'use strict';

    angular
        .module('app')
        .factory('categoryDataService', categoryDataService);

    categoryDataService.$inject = ['$http', 'api'];
    function categoryDataService($http, api) {
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
            return $http.get(api.category, { params: params });
        }
        function post(category) {
            return $http.post(api.category, category);
        }

        function remove(category) {
            return $http.delete(api.category + '/' + category.id);
        }
        function put(category) {
            return $http.put(api.category + '/' + category.id, category);
        }
        function putUpdateChildParent(categories, scheme) {
            return $http.put(api.category + '/UpdateChildParent/' + scheme.id, categories);
        }
    }
})();