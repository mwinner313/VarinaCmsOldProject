(function() {
    'use strict';

    angular
        .module('app.core')
        .factory('guid', guid);

    guid.$inject = [];
    function guid() {
        var service = {
            new:newGuid
        };
        
        return service;

        ////////////////
        function newGuid() {
            function s4() {
              return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
              s4() + '-' + s4() + s4() + s4();
          }
    }
})();