(function() {
    'use strict';

    angular
        .module('app.core')
        .controller('searchDialogCtrl', searchDialogCtrl);

    searchDialogCtrl.$inject = ['$mdDialog'];
    function searchDialogCtrl($mdDialog) {
        var vm = this;
        vm.searchText;
        vm.cancel=cancel;
        vm.ok=ok;

        function ok(){
              $mdDialog.hide(vm.searchText);
        }

        function cancel(){
              $mdDialog.cancel();
        }
    }
})();