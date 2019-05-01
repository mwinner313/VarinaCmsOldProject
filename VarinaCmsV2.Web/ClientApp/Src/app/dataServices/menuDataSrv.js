(function () {
    'use strict';

    angular
        .module('app')
        .factory('menuDataSrv', menuDataSrv);

    menuDataSrv.$inject = ['$http','api'];
    function menuDataSrv($http,api) {
        var service = {
            get: get,
            post: post,
            put: put,
            addMenuItems: addMenuItems,
            delete: remove,
        };

        return service;

        ////////////////
        function get(params) {
            return $http.get(api.menu, { params: params });
        }
        function post(menu) {
            return $http.post(api.menu, menu);
        }

        function put(menu) {
            return $http.put(api.menu + '/' + menu.id, menu);
        }

        function remove(id) {
            return $http.delete(api.menu + '/' + id);
        }

        function addMenuItems(menuItems){
            return $http.post(api.menu + '/addmenuitem', menuItems);
        }
    }
})();