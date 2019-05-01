(function () {
    'use strict';

    angular
        .module('app')
        .controller('FormDetailsCtrl', FormDetailsCtrl);

    FormDetailsCtrl.$inject = ['$mdDialog', 'formDataSrv'];
    function FormDetailsCtrl($mdDialog, formDataSrv) {
        var vm = this;
        vm.close = close;
        vm.changeIsSeenState = changeIsSeenState;
        activate();

        ////////////////

        function activate() {
        }
        function changeIsSeenState() {
            formDataSrv.changeIsSeenState(vm.form.id).then(function (res) {
                vm.form.isSeen = !vm.form.isSeen;
            });
        }
        function close() {
            $mdDialog.cancel();
        }
    }
})();