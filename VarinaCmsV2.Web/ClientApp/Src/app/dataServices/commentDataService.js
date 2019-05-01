(function () {
    'use strict';

    angular
        .module('app')
        .factory('commentDataSrv', commentDataSrv);

    commentDataSrv.$inject = ['$http', 'api'];
    function commentDataSrv($http, api) {
        var service = {
            getCommentDetail: getCommentDetail,
            get: get,
            put: put,
            delete: remove,
            approve: approve
        };

        return service;

        ////////////////
        function getCommentDetail(id) {
            return $http.get(api.comment + "/" + id);
        }

        function get(query) {
            return $http.get(api.comment, { params: query })
        }
        function put(cmt) {
            return $http.put(api.comment, cmt);
        }
        function remove(ids) {
            return $http.post(api.comment + "/DeleteBatch", ids);
        }

        function approve(ids) {
            return $http.post(api.comment + "/ChangeApproveState", ids);
        }
    }
})();