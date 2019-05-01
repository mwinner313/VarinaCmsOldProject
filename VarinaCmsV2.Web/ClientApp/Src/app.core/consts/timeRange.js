(function(){
    'use strict';

    angular
        .module('app.core')
        .constant('timeRange', {
            oneMonth:10,
            twoMonth:20,
            threeMonth:30,
            oneWeek:1,
            twoWeek:2,
            threeWeek:3,
            unlimited:0
        });

}());