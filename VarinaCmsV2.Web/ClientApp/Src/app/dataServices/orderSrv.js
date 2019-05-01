(function() {
    'use strict';

    angular
        .module('app')
        .factory('orderSrv', orderSrv);

    orderSrv.$inject = ['$http','api'];

    function orderSrv($http,api) {
        var service = {
            get: get,
            getById: getById,
            put: put,
            post: post,
            changeSeenStateByAdmin:changeSeenStateByAdmin,
            changeOrderItemDownLoadActivationState :changeOrderItemDownLoadActivationState ,
            sendOrderShippmentStatusChangedNotification:sendOrderShippmentStatusChangedNotification
        }

        return service;


        function getById(id) {
            return $http.get(api.order + '/' + id);
        }
        function get(query) {
            return $http.get(api.order, { params: query });
        }

        function post(order) {
            return $http.post(api.order, order);
        }

        function put(order) {
            return $http.put(api.order + '/' + order.id, order);
        }

        function changeSeenStateByAdmin(order) {
            return $http.patch(api.order + '/ChangeSeenStateByAdmin/' + order.id);
        }

        function changeOrderItemDownLoadActivationState(orderItem) {
            return $http.patch(api.order + '/ChangeOrderItemDownLoadActivationState/' + orderItem.id);
        }

        function sendOrderShippmentStatusChangedNotification(orderId) {
            return $http.patch(api.order + '/SendOrderShippmentStatusChangedNotification/' + orderId);
        }

    }
})();