(function () {
    'use strict';

    angular
        .module('app')
        .controller('ShaprakGateWaySettingCtrl', ShaprakGateWaySettingCtrl);

    ShaprakGateWaySettingCtrl.$inject = ['$mdDialog', 'settingSrv', '$mdToast'];

    function ShaprakGateWaySettingCtrl($mdDialog, settingSrv, $mdToast) {
        var vm = this;
        vm.closeDialog = closeDialog;
        vm.ok = ok;

        activate();

        ////////////////

        function activate() {}

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
            })
        }
    }
})();