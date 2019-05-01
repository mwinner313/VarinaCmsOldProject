; (function () {
    'use strict';
    angular
        .module('app')
        .constant('datetimeRegex', new RegExp(/^(\d{4})\/(\d{2})\/(\d{2})\s*-\s*(\d{2}|\d{1}):(\d{2}|\d{1})$/))
        .constant('dateRegex', new RegExp(/^(\d{4})\/(\d{2})\/(\d{2})$/));

    angular
        .module('app').constant('categoryType', {
            main: 1,
            secondary: 2,
            mixed: 0
        });
    angular
        .module('app').constant('menuItemTargetType', {
            customLink:1,
            entity:2,
            page:3,
            userProfile:4,
            product:5,
            category:6,
            productCategory:7
        });
        angular.module('app').constant('smallScrollOpts',{
            autoHideScrollbar: true,
            theme: 'dark',
            advanced: {
                updateOnContentResize: true
            },
            mouseWheel: {
                scrollAmount: 88
            },
            scrollButtons: {
                enable: false
            },
            scrollInertia: 800,
            setHeight: 280,
            autoDraggerLength: false,
            axis: 'y'
        })
})();