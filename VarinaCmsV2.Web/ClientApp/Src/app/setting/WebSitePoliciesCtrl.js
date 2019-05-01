(function () {
    'use strict';

    angular
        .module('app')
        .controller('WebSitePoliciesCtrl', WebSitePoliciesCtrl);

    WebSitePoliciesCtrl.$inject = ['$mdDialog', 'settingSrv', '$mdToast', 'roleSrv'];

    function WebSitePoliciesCtrl($mdDialog, settingSrv, $mdToast, roleSrv) {
        var vm = this;
        vm.closeDialog = closeDialog;
        vm.ok = ok;
        roleSrv.get({
            justNames: false
        }).then(function (res) {
            vm.roles = res.data;
        });

        activate();

        ////////////////

        function activate() {
            
        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        function ok() {
            if (vm.settingForm.$invalid) {
                $mdToast.showSimple("مقایر را بررسی کنید!");
                return;
            }
            vm.isSendingData = true
            settingSrv.put(vm.setting).then(function (res) {
                if (res.status == 200) {
                    $mdToast.showSimple("انجام شد!")
                    $mdDialog.hide();
                }
            });
        }
    }
})();