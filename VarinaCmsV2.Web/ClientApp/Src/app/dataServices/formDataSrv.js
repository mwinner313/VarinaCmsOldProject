(function () {
    'use strict';

    angular
        .module('app')
        .factory('formDataSrv', formDataSrv);

    formDataSrv.$inject = ['$http', 'api', '$q'];
    function formDataSrv($http, api, $q) {
        var subscriberCbs = [];
        var service = {
            get: get,
            changeIsSeenState: patch,
            addSubscriberFunction: addSubscriberFunction
        };

        return service;

        ////////////////
        function get(params) {
            return $http.get(api.form, { params: params });
        }
        function patch(id) {
            var defered = $q.defer();
            $http.patch(api.form + "/" + id).then(function (res) {
                defered.resolve(res);
                angular.forEach(subscriberCbs, function (cb) {
                    cb();
                })
            }, function (err) {
                defered.reject(err);
            });
            return defered.promise;
        }
        function addSubscriberFunction(cb) {
            subscriberCbs.push(cb);
        }
    }
})();