(function () {
    'use strict';

    angular
        .module('app')
        .controller('menuWidgetCtrl', menuWidgetCtrl);

    menuWidgetCtrl.$inject = ['$mdDialog', 'menuDataSrv', 'identityManager', 'lodash','$q'];
    function menuWidgetCtrl($mdDialog, menuDataSrv, identityManager, lodash,$q) {
        var vm = this;
        vm.widget = vm.widget || { metaData: {}, type: 'menu' };
        vm.selectedMenu = { id: vm.widget.metaData.menuId };
        vm.menus = [];
        vm.ok = ok;
        vm.isInRole = isInRole;
        vm.closeDialog = closeDialog;

        activate();

        ////////////////

        function activate() {
            var defered=$q.defer();
            vm.tableProgressPromise=defered.promise;
            menuDataSrv.get().then(function (res) {
                vm.menus = res.data;
                defered.resolve();
            });
        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        function ok() {
            vm.widget.metaData.menuId = vm.selectedMenu.id;
            vm.widget.title = vm.widget.title || vm.selectedMenu.title;
            $mdDialog.hide(vm.widget);
        }

        function isInRole(role) {
            return identityManager.isInRole(role);
        }
    }
})();