(function() {
    'use strict';

    angular
        .module('app')
        .controller('userProfileWidgetCtrl', userProfileWidgetCtrl);

    userProfileWidgetCtrl.$inject = ['$mdDialog','userDataSrv','$q'];
    function userProfileWidgetCtrl($mdDialog,userDataSrv,$q) {
        var vm = this;
        vm.widget=vm.widget||{type:'userProfile',metaData:{}};
        vm.selectedUser = { id: vm.widget.metaData.userId }
        vm.ok=ok;
        vm.closeDialog=closeDialog;
        activate();

        ////////////////

        function activate() { 
            var defered=$q.defer();
            vm.tableProgressPromise=defered.promise;
            userDataSrv.get().then(function(res){
                vm.users=res.data.items;
                defered.resolve();
            });
        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        function ok() {
            vm.widget.metaData.userId = vm.selectedUser.id;
            vm.widget.title = vm.widget.title || vm.selectedUser.name;
            $mdDialog.hide(vm.widget);
        }
    }
})();