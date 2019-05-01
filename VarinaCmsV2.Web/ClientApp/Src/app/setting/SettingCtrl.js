(function () {
    'use strict';

    angular
        .module('app')
        .controller('SettingCtrl', SettingCtrl);

    SettingCtrl.$inject = ['settingSrv', '$mdDialog', 'settings'];

    function SettingCtrl(settingSrv, $mdDialog, settings) {
        var vm = this;
        vm.showSettingDialog=showSettingDialog;
        vm.settings = settings.data;
        activate();

        ////////////////

        function activate() {
        }
        function  showSettingDialog(setting){
            $mdDialog.show({
                templateUrl: ('/Src/app/setting/' + setting.name + '.tpl.html'),
                controller: (setting.name + 'Ctrl'),
                controllerAs: 'vm',
                bindToController: true,
                clickOutsideToClose: true,
                escapeToClose: true,
                fullscreen: true,
                locals: {
                    setting : setting,
                }
            })
        }
    }
})();