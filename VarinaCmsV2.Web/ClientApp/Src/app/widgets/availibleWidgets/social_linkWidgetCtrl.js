(function(){
    'use strict';

    angular
        .module('app')
        .controller('social_linkWidgetCtrl', social_linkWidgetCtrl)

    social_linkWidgetCtrl.$inject = ['$mdDialog','guid','identityManager'];

    function social_linkWidgetCtrl($mdDialog,guid,identityManager) {
        /* jshint validthis:true */
        var vm = this;
        vm.widget = vm.widget || { metaData: {}, type: 'social_link' }
        vm.ok=ok;
        vm.closeDialog=closeDialog;
        vm.isInRole=identityManager.isInRole;

        function ok(){
            vm.widget.title = vm.widget.title || 'لینک شبکه های اجتماعی';
            vm.widget.handle=vm.widget.handle || guid.new();
            $mdDialog.hide(vm.widget);
        }
         
        function closeDialog(){
            $mdDialog.cancel();
        }
    }
})();