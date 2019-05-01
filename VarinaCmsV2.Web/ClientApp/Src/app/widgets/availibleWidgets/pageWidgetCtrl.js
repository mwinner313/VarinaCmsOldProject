(function () {
    'use strict';

    angular
        .module('app')
        .controller('pageWidgetCtrl', pageWidgetCtrl);

    pageWidgetCtrl.$inject = ['$mdDialog', 'pageDataService','identityManager','$q'];
    function pageWidgetCtrl($mdDialog, pageDataService,identityManager,$q) {
        var vm = this;
        vm.widget = vm.widget || { metaData: {}, type: 'page' }
        vm.selectedPage = { id: vm.widget.metaData.pageId }
        vm.ok=ok;
        vm.closeDialog=closeDialog;
        vm.isInRole=isInRole;
        activate();

        ////////////////

        function activate() {
            var defered=$q.defer();
            vm.tableProgressPromise=defered.promise;
            pageDataService.get().then(function(res){
                vm.pages=res.data.items;
                defered.resolve();
            })
        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        function ok() {
            vm.widget.metaData.pageId = vm.selectedPage.id;
            vm.widget.title = vm.widget.title || vm.selectedPage.title;
            $mdDialog.hide(vm.widget);
        }

        function isInRole(role) {
            return identityManager.isInRole(role);
        }
    }
})();