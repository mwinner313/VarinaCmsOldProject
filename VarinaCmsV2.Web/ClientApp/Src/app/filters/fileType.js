(function() {
    'use strict';

    angular
        .module('app')
        .filter('filetype', fileType);

    function fileType() {
        return fileTypeFilter;

        ////////////////

        function fileTypeFilter(Params) {
           switch(Params){
               case '.jpeg':
               case '.jpg' :return 'تصویر'
           }
        }
    }
})();