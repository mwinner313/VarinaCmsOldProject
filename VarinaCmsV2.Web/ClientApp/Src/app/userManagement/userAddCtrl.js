(function() {
    'use strict';

    angular
        .module('app')
        .controller('userAddCtrl', userAddCtrl);

    userAddCtrl.$inject = ['$mdDialog','$mdToast','userDataSrv'];
    function userAddCtrl($mdDialog,$mdToast,userDataSrv) {
        var vm = this;
        vm.user={};
        vm.ok=ok;
        vm.cancel=cancel;
        activate();

        ////////////////

        function activate() { }

        function ok(){
            vm.userForm.$submitted=true;
            if(vm.userForm.$invalid){
                $mdToast.showSimple('مقادیر را بررسی کنید');
                return;
            }
            vm.isSendingData=true;
            userDataSrv.add(vm.user).then(function(res){
                $mdDialog.hide(res.data);
            });
        }

        function cancel(){
            $mdDialog.cancel();
        }
    }
})();