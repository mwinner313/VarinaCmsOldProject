(function () {
    'use strict';

    angular
        .module('app')
        .factory('requestLanguageInterceptor', requestLanguageInterceptor);

    requestLanguageInterceptor.$inject = ['currentLang'];
    function requestLanguageInterceptor(currentLang) {
        var service = {
            request: request
        };

        return service;
        ////////////////
        function request(config) {
            if(!config.url.startsWith('/api'))return config || $q.when(config);
            config.params = config.params || {};
            config.params.languageName = currentLang.get();
            return config || $q.when(config);
        }
    }
})();