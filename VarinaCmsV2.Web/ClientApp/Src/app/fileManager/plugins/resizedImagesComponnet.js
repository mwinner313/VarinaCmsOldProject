(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('resizedImages', {
            templateUrl: '/Src/app/fileManager/plugins/resizedImages-tpl.html',
            controller: resizedPlugin,
            controllerAs: 'vm',
            bindings: {
                item: '<',
                select:'&'
            },
        });

    resizedPlugin.$inject = ['lodash'];
    function resizedPlugin(lodash) {
        var vm = this;
        vm.selectResized=selectResized;
        vm.metaName = 'resized-image'

        ////////////////

        vm.$onInit = function () {
        };
        vm.$onChanges = function (changesObj) {
            if(!changesObj.item.isFirstChange()){
                vm.resizedImages = lodash.filter(changesObj.item.currentValue.metaData, function (meta) {
                    return meta.metaName === vm.metaName;
                });
            }
        };
        vm.$onDestroy = function () { };
        function selectResized(resized){
           vm.select()(resized.metaData);
        }
    }
})();