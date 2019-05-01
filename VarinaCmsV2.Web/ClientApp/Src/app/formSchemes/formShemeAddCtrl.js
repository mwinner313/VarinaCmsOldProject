(function () {
    'use strict';

    angular
        .module('app')
        .controller('formSchemeAddCtrl', formAddCtrl);

    formAddCtrl.$inject = ['formSchemeDataSrv', 'formFieldTypes', '$mdToast', '$state', '$mdDialog', '$rootScope'];
    function formAddCtrl(formSchemeDataSrv, formFieldTypes, $mdToast, $state, $mdDialog, $rootScope) {
        var vm = this;
        vm.formScheme = {
            fieldDefenitions: []
        }
        vm.fieldTypes = angular.copy(formFieldTypes);
        vm.resetAvailibleFds = resetAvailibleFds;
        vm.save = save;
        vm.showRemoveFdDialog = showRemoveFdDialog;
        vm.beforeFdDropInlist = beforeFdDropInlist;
        activate();

        ////////////////

        function activate() { }

        function resetAvailibleFds() {
            vm.fieldTypes = angular.copy(formFieldTypes);
        }

        function save() {
            angular.forEach(vm.formScheme.fieldDefenitions, function (fd, idx) {
                vm.formScheme.fieldDefenitions[idx].order = idx;
            });
            formSchemeDataSrv.post(vm.formScheme).then(function (res) {
                $mdToast.showSimple('انجام شد');
                $state.go('formSchemes', { pageNumber: 1 ,lang:$rootScope.currentLang});
            });
        }
        function showRemoveFdDialog(fd,event) {
            var confirm = $mdDialog.confirm()
                .title('از حذف این مورد اطمینان دارید؟')
                .targetEvent(event)
                .textContent(fd.title)
                .ok('بلی')
                .cancel('خیر');
            $mdDialog.show(confirm).then(function (res) {
                var idx = vm.formScheme.fieldDefenitions.indexOf(fd);
                vm.formScheme.fieldDefenitions.splice(idx, 1);
            });
        }

        function beforeFdDropInlist(fd) {
            return fd;
        }
    }
})();