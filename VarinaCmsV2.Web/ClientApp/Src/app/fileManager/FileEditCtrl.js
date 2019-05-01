(function () {
    'use strict';

    angular
        .module('app')
        .controller('FileEditCtrl', FileEditCtrl);

    FileEditCtrl.$inject = ['$mdDialog', 'fileDataSrv', '$mdToast'];
    function FileEditCtrl($mdDialog, fileDataSrv, $mdToast) {
        var vm = this;
        vm.edit = edit
        vm.isShowLoader = false;
        vm.closeDialog = closeDialog;
        activate();

        ////////////////

        function activate() { }

        function edit() {
            vm.isShowLoader = true;
            fileDataSrv.put(vm.file).then(function (res) {
                $mdToast.showSimple('انجام شد !');
                vm.isShowLoader = false;
                $mdDialog.hide(res.data);
            });
        }
        function closeDialog() {
            $mdDialog.cancel();
        }
    }
})();