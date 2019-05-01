(function () {
    'use strict';

    angular
        .module('app')
        .controller('WebSiteBasicInformationCtrl', WebSiteBasicInformationCtrl);

    WebSiteBasicInformationCtrl.$inject = ['$mdDialog', 'settingSrv', '$mdToast'];

    function WebSiteBasicInformationCtrl($mdDialog, settingSrv, $mdToast) {
        var vm = this;
        vm.closeDialog = closeDialog;
        vm.ok = ok;
        vm.codemirrorOptions = {
            lineNumbers: true,
            lineWrapping: true,
            mode: 'xml',
            theme: 'material',
            autoCloseTags: true,
            styleActiveLine: true,
            extraKeys: {
                "Ctrl-Space": "autocomplete"
            },
        };

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
            });
        }
    }
})();