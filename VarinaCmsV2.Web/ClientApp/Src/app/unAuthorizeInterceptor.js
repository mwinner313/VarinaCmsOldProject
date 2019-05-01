;
(function () {
    'use strict';

    angular
        .module('app')
        .factory('unAuthorizeInterceptor', unAuthorizeInterceptor);

    unAuthorizeInterceptor.$inject = ['$injector', 'api','$q'];

    function unAuthorizeInterceptor($injector, api,$q) {
        var service = {
            responseError: responseError,
        };

        return service;

        ////////////////
        function responseError(err) {
            if ([404, 400, 500].indexOf(err.status) !== -1) {
                console.log(err);
               alert('خطا لطفا با اپراتور تماس بگیرید !');
            }

            if (err.status == 401) {
                location.href = '/login?returnUrl=/panel';
            }

            return err;
        }
    }
})();