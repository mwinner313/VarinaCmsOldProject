;(function () {
    'use strict';

    angular
        .module('app')
        .constant('bottomSheetOptions', {
            templateUrl: '/Src/app/partials/bottomSheetOptions-tpl.html',
            controller: 'bottomSheetCtrl',
            controllerAs: 'vm',
            bindToController: true,
        });


})();