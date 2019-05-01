(function() {
    'use strict';

    angular
        .module('app')
        .controller('entityWidgetCtrl', entityWidgetCtrl);

    entityWidgetCtrl.$inject = ['$mdDialog','$q','schemeDataService','identityManager','lodash','orderOption','timeRange'];
    function entityWidgetCtrl($mdDialog,$q,schemeDataService,identityManager,lodash,orderOption,timeRange) {
        var vm = this;
        vm.widget = vm.widget || { metaData: {count:10}, type: 'entity' }
        vm.selectedScheme = { id: vm.widget.metaData.schemeId }
        vm.ok=ok;
        vm.closeDialog=closeDialog;
        vm.isInRole=isInRole;
        vm.orderOption=orderOption;
        vm.timeRange=timeRange;
        activate();

        ////////////////
        function activate() {
            var defered=$q.defer();
            vm.tableProgressPromise=defered.promise;
            schemeDataService.get({type:5}).then(function(res){
                vm.schemes=res.data;
                defered.resolve();
                if (vm.selectedScheme.id) {
                    vm.selectedScheme = lodash.find(res.data, function (scheme) { return scheme.id == vm.selectedScheme.id });
                }
            })
        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        function ok() {
            vm.widget.metaData.schemeId = vm.selectedScheme.id;
            vm.widget.title = ['', null, undefined].indexOf(vm.widget.title) === -1 ? vm.widget.title : 'آخرین مطالب' + ' | '  + vm.selectedScheme.title;
            $mdDialog.hide(vm.widget);
        }

        function isInRole(role) {
            return identityManager.isInRole(role);
        }
    }
})();