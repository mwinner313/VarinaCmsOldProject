(function(){
    'use strict';

    angular
        .module('app.core')
        .constant('orderOption', {
            byDateDecending:1,
            random:2,
            visitNumber:3,
            likesCount:4
        });

}());