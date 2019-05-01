(function () {
    'use strict';

    angular
        .module('app')
        .controller('userEditCtrl', userEditCtrl);

    userEditCtrl.$inject = ['$mdDialog','userDataSrv','$mdToast'];
    function userEditCtrl($mdDialog,userDataSrv,$mdToast ) {
        var vm = this;
        vm.ok = ok;
        vm.cancel = cancel;

        activate();

        ////////////////

        function activate() {}
        function ok() {
            vm.userForm.$submitted = true;
            if (vm.userForm.$invalid) {
                $mdToast.showSimple('مقادیر را بررسی کنید');
                return;
            }
            vm.isSendingData=true;
            userDataSrv.update(vm.user).then(function (res) {
                $mdDialog.hide(vm.user);
            });
        }

        function cancel() {
            $mdDialog.cancel();
        }
    }
})();